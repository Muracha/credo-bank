using AutoMapper;
using credo_bank.Application.Helper;
using credo_bank.Application.Settings;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace credo_bank.Application.MediatR.User.Commands.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, ApiServiceResponse<RefreshTokenResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;
    public RefreshTokenHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<ApiServiceResponse<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(request.Token, cancellationToken: cancellationToken);
        if (user is null)
            return ApiServiceResponse<RefreshTokenResult>.FailureResponse("Invalid refresh token");

        var refreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == request.Token);
        if (refreshToken is null || refreshToken.Invalidated || refreshToken.ExpirationDate < DateTime.UtcNow)
            return ApiServiceResponse<RefreshTokenResult>.FailureResponse("Refresh token is invalid or expired");

        var newTokens = await JwtGenerator.GenerateTokens(user, _jwtSettings, _refreshTokenRepository, cancellationToken);

        refreshToken.InvalidateToken();
        await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken: cancellationToken);
        
        return ApiServiceResponse<RefreshTokenResult>.SuccessResponse(new RefreshTokenResult(newTokens), "Refresh token renewed successfully");
    }
}