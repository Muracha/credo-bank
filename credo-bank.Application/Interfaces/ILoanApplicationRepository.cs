using credo_bank.Domain.Models;

namespace credo_bank.Application.Interfaces;

public interface ILoanApplicationRepository
{
    Task<int> AddLoanApplicationAsync(LoanApplication? loan,
        CancellationToken cancellationToken = default);

    Task<LoanApplication?> GetLoanWithId(int loanId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateLoanApplicationAsync(LoanApplication? loan,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteLoanApplicationAsync(LoanApplication? loan,
        CancellationToken cancellationToken = default);

    Task<List<LoanApplication>> GetLoanApplicationsByUserIdAsync(int userId,
        CancellationToken cancellationToken = default);
}