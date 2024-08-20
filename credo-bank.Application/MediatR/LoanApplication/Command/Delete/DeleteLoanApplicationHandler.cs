using credo_bank.Application.Interfaces;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Delete;

public class DeleteLoanApplicationHandler : IRequestHandler<DeleteLoanApplicationCommand, ApiWrapper<DeleteLoanApplicationResult>>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    public DeleteLoanApplicationHandler(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }
    
    public async Task<ApiWrapper<DeleteLoanApplicationResult>> Handle(DeleteLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        var loanApplication = await _loanApplicationRepository.GetLoanWithId(request.LoanId, cancellationToken: cancellationToken);
        
        if (loanApplication == null)
            return ApiWrapper<DeleteLoanApplicationResult>.FailureResponse("Loan application not found");
        
        var IsDeleted = await _loanApplicationRepository.DeleteLoanApplicationAsync(loanApplication, cancellationToken: cancellationToken);
        
        return ApiWrapper<DeleteLoanApplicationResult>.SuccessResponse(new DeleteLoanApplicationResult(IsDeleted), "Loan application deleted successfully");
    }
}