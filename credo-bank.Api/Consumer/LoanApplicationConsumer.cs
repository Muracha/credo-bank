using credo_bank.Application.Interfaces;
using credo_bank.Application.Models.DTO.Messages;
using MassTransit;

namespace credo_bank.Consumer;

public class LoanApplicationConsumer : IConsumer<LoanApplicationSubmitted>
{ 
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    public LoanApplicationConsumer(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }

    public async Task Consume(ConsumeContext<LoanApplicationSubmitted> context)
    {
        var message = context.Message;
        var loan = await _loanApplicationRepository.GetLoanWithId(message.LoanId);
        
        if (loan != null && loan.ApplicationStatus == Domain.Enums.Application.SENT)
        {
            loan.ApplicationStatus = Domain.Enums.Application.INPROGRESS;
            await _loanApplicationRepository.UpdateLoanApplicationAsync(loan);
        }
    }
}