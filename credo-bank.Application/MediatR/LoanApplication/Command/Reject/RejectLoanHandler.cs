using credo_bank.Application.Interfaces;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Reject;

public class RejectLoanHandler : IRequestHandler<RejectLoanCommand, ApiWrapper<RejectLoanResult>>
{
    private readonly ILoanApplicationRepository _repository;
    public RejectLoanHandler(ILoanApplicationRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ApiWrapper<RejectLoanResult>> Handle(RejectLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _repository.GetLoanWithId(request.LoanId, cancellationToken);
        if (loan != null && loan.ApplicationStatus == Domain.Enums.Application.CANCELLED)
        {
            loan.ApplicationStatus = Domain.Enums.Application.CANCELLED;
            var result =  await _repository.UpdateLoanApplicationAsync(loan, cancellationToken);
            
            return ApiWrapper<RejectLoanResult>.SuccessResponse(new RejectLoanResult(result), "Loan application has been cancelled successfully");
        }
       
        return ApiWrapper<RejectLoanResult>.FailureResponse("Loan application was not found");
    }
}