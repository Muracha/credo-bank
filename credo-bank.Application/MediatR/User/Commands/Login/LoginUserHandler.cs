using AutoMapper;
using credo_bank.Application.Helper;
using credo_bank.Application.Settings;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace credo_bank.Application.MediatR.User.Commands.Login;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, ApiWrapper<LoginUserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;
    public LoginUserHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<ApiWrapper<LoginUserResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdentificationNumber(request.IdentificationNumber, cancellationToken: cancellationToken);
        if (user is null)
            return ApiWrapper<LoginUserResult>.FailureResponse("Invalid Identification number or password");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return ApiWrapper<LoginUserResult>.FailureResponse("Invalid Identification number or password");
        
        var token = await JwtGenerator.GenerateTokens(user, _jwtSettings, _refreshTokenRepository, cancellationToken);

        return ApiWrapper<LoginUserResult>.SuccessResponse(new LoginUserResult(token), "User logged in successfully");
    }
}