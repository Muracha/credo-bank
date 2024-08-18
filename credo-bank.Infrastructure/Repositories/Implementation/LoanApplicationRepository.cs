using credo_bank.DAL.DataContext;
using credo_bank.DAL.Repositories.Interfaces;
using credo_bank.Domain.Models;

namespace credo_bank.DAL.Repositories.Implementation;

public class LoanApplicationRepository : BaseRepository, ILoanApplicationRepository
{
    public LoanApplicationRepository(CredoBankDbContext context) : base(context) { }
    
    public async Task<int> AddLoanApplicationAsync(LoanApplication loan,
        CancellationToken cancellationToken = default)
    {
        _context.LoanApplications.Add(loan);
        await _context.SaveChangesAsync(cancellationToken : cancellationToken);
        return loan.Id;
    }
    
    public async Task<LoanApplication> GetLoanWithId(int loanId,
        CancellationToken cancellationToken = default)
        => await _context.LoanApplications.FindAsync(new Object[loanId], cancellationToken : cancellationToken);

    public async Task<bool> UpdateLoanApplicationAsync(LoanApplication loan,
        CancellationToken cancellationToken = default)
    {
        _context.LoanApplications.Update(loan);
        return await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }
}