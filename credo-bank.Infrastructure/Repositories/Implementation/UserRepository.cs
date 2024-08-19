using credo_bank.DAL.Repositories.Interfaces;
using credo_bank.Domain.Models;
using credo_bank.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace credo_bank.DAL.Repositories.Implementation;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(CredoBankDbContext context) : base(context) { }
    
    public async Task<int> AddUserAsync(User? user,
        CancellationToken cancellationToken = default)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken : cancellationToken);
        return user.Id;
    }
    
    public async Task<IQueryable<User?>> GetAllUsersAsync(
        CancellationToken cancellationToken = default)
        => _context.Users.AsNoTracking();
    
    public async Task<User?> GetUserByIdAsync(int userId,
        CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().Where(x => x.Id == userId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<User> GetUserWithLoans(int userId,
        CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking()
            .Include(x => x.LoanApplications)
            .Where(x => x.Id == userId)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<User> GetUserByIdentificationNumber(int number,
        CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking().Where(x => x.IdentificationNumber == number)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
    
    public async Task<bool> UpdateUserAsync(User? user,
        CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }
    
    public async Task<User> GetByRefreshTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default)
        => await _context.Users.AsNoTracking()
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken),
                cancellationToken: cancellationToken);
}