using credo_bank.DAL.DataContext;
using credo_bank.DAL.Repositories.Interfaces;
using credo_bank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace credo_bank.DAL.Repositories.Implementation;

public class RefreshTokenRepository : BaseRepository, IRefreshTokenRepository
{
    public RefreshTokenRepository(CredoBankDbContext credoBankDbContext) : base(credoBankDbContext) { }
    
    public async Task<bool> SaveRefreshTokenAsync(RefreshToken refreshTokenEntity,
        CancellationToken cancellationToken = default)
    {
        _ = await _context.RefreshTokens.AddAsync(refreshTokenEntity, cancellationToken: cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }

    public Task<RefreshToken?> GetRefreshTokenByValueAsync(string tokenValue,
        CancellationToken cancellationToken = default)
        => _context.RefreshTokens.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Token == tokenValue, cancellationToken: cancellationToken);
    
    public async Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync();
    }
}