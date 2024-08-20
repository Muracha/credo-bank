using System.Security.Cryptography;
using credo_bank.Application.Interfaces;
using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Response;
using credo_bank.Application.Settings;
using credo_bank.Application.Utilities.Jwt;
using credo_bank.Domain.Models;

namespace credo_bank.Application.Helper;

public static class JwtGenerator
{
    public static async Task<AuthReposnoseDto> GenerateTokens(Domain.Models.User user, JwtSettings jwtSetting, IRefreshTokenRepository refreshTokenRepository, 
        CancellationToken cancellationToken)
    {
        var jwtToken = JwtUtility.GenerateJwtToken(user, jwtSetting);
        var refreshToken = CreateRefreshTokenEntity(user, GenerateRefreshToken());

        await refreshTokenRepository.SaveRefreshTokenAsync(refreshToken, cancellationToken: cancellationToken);

        return new AuthReposnoseDto(jwtToken, refreshToken.Token);
    }

    private static RefreshToken CreateRefreshTokenEntity(Domain.Models.User user, string refreshToken)
    {
        return new RefreshToken
        {
            Token = refreshToken,
            JwtId = Guid.NewGuid().ToString(),
            CreatedDate = DateTime.Now,
            ExpirationDate = DateTime.Now.AddDays(7),
            Invalidated = false,
            UserId = user.Id
        };
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}