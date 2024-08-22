using credo_bank.Application.Interfaces;
using credo_bank.Application.Models.DTO.Messages;
using credo_bank.Domain.Models;
using credo_bank.Infrastructure.DataContext;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace credo_bank.Infrastructure.Repositories.Implementation;

public class LoanApplicationRepository : BaseRepository, ILoanApplicationRepository
{
    private readonly IPublishEndpoint _publishEndpoint;
    public LoanApplicationRepository(CredoBankDbContext context, IPublishEndpoint publishEndpoint) : base(context)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task<int> AddLoanApplicationAsync(LoanApplication? loan,
        CancellationToken cancellationToken = default)
    {
        await _context.LoanApplications.AddAsync(loan);
        
        await _context.SaveChangesAsync(cancellationToken : cancellationToken);
        
        await _publishEndpoint.Publish(new LoanApplicationSubmitted
        {
            LoanId = loan.Id,
            UserId = loan.UserId,
            LoanType = loan.LoanType,
            LoanAmount = loan.LoanAmount,
            CurrencyType = loan.CurrencyType,
            LoanTermInMonths = loan.LoanTermInMonths,
            ApplicationStatus = loan.ApplicationStatus
        }, cancellationToken);
        
        return loan.Id;
    }
    
    public async Task<List<LoanApplication>> GetLoanApplicationsByUserIdAsync(int userId,
        CancellationToken cancellationToken = default)
        => await _context.LoanApplications
            .Where(x => x.IsDeleted == false)
            .Where(x => x.UserId == userId)
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
        loan.IsDeleted = true;
        
        _context.LoanApplications.Update(loan);
        
        return await _context.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
    }
}