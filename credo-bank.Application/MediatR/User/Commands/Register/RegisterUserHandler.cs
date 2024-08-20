using AutoMapper;
using credo_bank.Application.Helper;
using credo_bank.Application.Interfaces;
using credo_bank.Application.Settings;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;
using Microsoft.Extensions.Options;

namespace credo_bank.Application.MediatR.User.Commands.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ApiWrapper<RegisterUserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;
    public RegisterUserHandler(IUserRepository userRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<ApiWrapper<RegisterUserResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserByIdentificationNumber(request.IdentificationNumber, cancellationToken: cancellationToken);
        
        if (existingUser != null)
            return ApiWrapper<RegisterUserResult>.FailureResponse();

        var user = new Domain.Models.User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            IdentificationNumber = request.IdentificationNumber,
            DateOfBirth = request.DateOfBirth,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        
        await _userRepository.AddUserAsync(user, cancellationToken);
        var generatedToken = await JwtGenerator.GenerateTokens(user, _jwtSettings, _refreshTokenRepository, cancellationToken: cancellationToken);
        
        return ApiWrapper<RegisterUserResult>.SuccessResponse(new RegisterUserResult(generatedToken));
    }
}