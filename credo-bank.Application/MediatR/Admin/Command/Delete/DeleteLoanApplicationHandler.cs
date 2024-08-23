using credo_bank.Application.Interfaces;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.Admin.Command.Delete;

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

        if (loanApplication.ApplicationStatus is Domain.Enums.Application.SENT or Domain.Enums.Application.INPROGRESS)
        {
            return ApiWrapper<DeleteLoanApplicationResult>.FailureResponse("Loan application can not be deleted while in progress or sent position.");
        }
        
        var IsDeleted = await _loanApplicationRepository.DeleteLoanApplicationAsync(loanApplication, cancellationToken: cancellationToken);
        
        return ApiWrapper<DeleteLoanApplicationResult>.SuccessResponse(new DeleteLoanApplicationResult(IsDeleted), "Loan application deleted successfully");
    }
}