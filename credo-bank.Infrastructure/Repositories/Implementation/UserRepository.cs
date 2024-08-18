using credo_bank.DAL.DataContext;
using credo_bank.DAL.Repositories.Interfaces;
using credo_bank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace credo_bank.DAL.Repositories.Implementation;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(CredoBankDbContext context) : base(context) { }
    
    public async Task<int> AddUserAsync(User user,
        CancellationToken cancellationToken = default)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken : cancellationToken);
        return user.Id;
    }

    public async Task<User> GetUserByIdAsync(int userId,
        CancellationToken cancellationToken = default)
        => await _context.Users.FindAsync(new Object[userId], cancellationToken : cancellationToken);

    public async Task<User> GetUserWithLoans(int userId,
        CancellationToken cancellationToken = default)
        => await _context.Users.Include(x => x.LoanApplications)
            .SingleOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);
    
    public async Task<User> GetUserByIdentificationNumber(int number,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users.AsNoTracking().Where(x => x.IdentificationNumber == number)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<bool> UpdateUserAsync(User user,
        CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }
}