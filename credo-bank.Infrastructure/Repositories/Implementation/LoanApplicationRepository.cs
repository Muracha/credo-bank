using credo_bank.Domain.Models;
using credo_bank.Infrastructure.DataContext;
using credo_bank.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace credo_bank.Infrastructure.Repositories.Implementation;

public class LoanApplicationRepository : BaseRepository, ILoanApplicationRepository
{
    public LoanApplicationRepository(CredoBankDbContext context) : base(context) { }
    
    public async Task<int> AddLoanApplicationAsync(LoanApplication? loan,
        CancellationToken cancellationToken = default)
    {
        _context.LoanApplications.Add(loan);
        await _context.SaveChangesAsync(cancellationToken : cancellationToken);
        return loan.Id;
    }
    
    public async Task<List<LoanApplication>> GetLoanApplicationsByUserIdAsync(int userId,
        CancellationToken cancellationToken = default)
        => await _context.LoanApplications.Where(x => x.IsDeleted == false).Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken: cancellationToken);
    
    public async Task<LoanApplication?> GetLoanWithId(int loanId,
        CancellationToken cancellationToken = default)
        => await _context.LoanApplications
            .Where(x => x.Id == loanId)
            .Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

    public async Task<bool> UpdateLoanApplicationAsync(LoanApplication? loan,
        CancellationToken cancellationToken = default)
    {
        _context.LoanApplications.Update(loan);
        return await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }
    
    public async Task<bool> DeleteLoanApplicationAsync(LoanApplication? loan,
        CancellationToken cancellationToken = default)
    {
        _context.LoanApplications.Remove(loan);
        return await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }
}