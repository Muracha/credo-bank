using AutoMapper;
using credo_bank.Application.Interfaces;
using credo_bank.Application.MediatR.LoanApplication.Command.Update;
using credo_bank.Application.Models.DTO.Response;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.Admin.Command.Update;

public class UpdateLoanApplicationAdminHandler : IRequestHandler<UpdateLoanApplicationAdminCommand, ApiWrapper<UpdateLoanApplicationAdminResult>>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IMapper _mapper;
    public UpdateLoanApplicationAdminHandler(ILoanApplicationRepository loanApplicationRepository, IMapper mapper)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _mapper = mapper;
    }

    public async Task<ApiWrapper<UpdateLoanApplicationAdminResult>> Handle(UpdateLoanApplicationAdminCommand request, CancellationToken cancellationToken)
    {
        var oldLoan = await _loanApplicationRepository.GetLoanWithId(request.LoanId, cancellationToken: cancellationToken);
        
        if (oldLoan == null)
            return ApiWrapper<UpdateLoanApplicationAdminResult>.FailureResponse("Loan not found");

        if (oldLoan.ApplicationStatus != Domain.Enums.Application.INPROGRESS)
        {
            return ApiWrapper<UpdateLoanApplicationAdminResult>.FailureResponse("Loan must be in progress to be updated");
        }
        
        oldLoan.LoanAmount = request.LoanAmount;
        oldLoan.LoanTermInMonths = request.LoanTermInMonths;
        
        await _loanApplicationRepository.UpdateLoanApplicationAsync(oldLoan, cancellationToken: cancellationToken);
        
        var newLoan = _mapper.Map<LoanApplicationDto>(oldLoan);
        
        return ApiWrapper<UpdateLoanApplicationAdminResult>.SuccessResponse(new UpdateLoanApplicationAdminResult(newLoan));
    }
}