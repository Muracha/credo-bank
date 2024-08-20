using credo_bank.Domain.Models;

namespace credo_bank.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task<bool> SaveRefreshTokenAsync(RefreshToken refreshTokenEntity,
        CancellationToken cancellationToken = default);

    Task<RefreshToken?> GetRefreshTokenByValueAsync(string tokenValue,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}