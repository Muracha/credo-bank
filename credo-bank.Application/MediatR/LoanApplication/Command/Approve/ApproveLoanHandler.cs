using credo_bank.Application.Interfaces;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Approve;

public class ApproveLoanHandler : IRequestHandler<ApproveLoanCommand, ApiWrapper<ApproveLoanResult>>
{
    private readonly ILoanApplicationRepository _repository;
    public ApproveLoanHandler(ILoanApplicationRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ApiWrapper<ApproveLoanResult>> Handle(ApproveLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _repository.GetLoanWithId(request.LoanId, cancellationToken);
        if (loan != null && loan.ApplicationStatus == Domain.Enums.Application.INPROGRESS)
        {
            loan.ApplicationStatus = Domain.Enums.Application.ACCEPTED;
            
            var result =  await _repository.UpdateLoanApplicationAsync(loan, cancellationToken);
            
            return ApiWrapper<ApproveLoanResult>.SuccessResponse(new ApproveLoanResult(result), "Loan application has been accepted successfully");
        }
        
        return ApiWrapper<ApproveLoanResult>.FailureResponse("Loan application was not found");
    }
}