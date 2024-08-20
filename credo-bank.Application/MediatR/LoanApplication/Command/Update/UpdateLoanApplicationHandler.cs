using AutoMapper;
using credo_bank.Application.Interfaces;
using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Response;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Update;

public class UpdateLoanApplicationHandler : IRequestHandler<UpdateLoanApplicationCommand, ApiWrapper<UpdateLoanApplicationResult>>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IMapper _mapper;
    public UpdateLoanApplicationHandler(ILoanApplicationRepository loanApplicationRepository, IMapper mapper)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _mapper = mapper;
    }
    
    public async Task<ApiWrapper<UpdateLoanApplicationResult>> Handle(UpdateLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        var oldLoan = await _loanApplicationRepository.GetLoanWithId(request.LoanId, cancellationToken: cancellationToken);
        
        if (oldLoan == null)
            return ApiWrapper<UpdateLoanApplicationResult>.FailureResponse("Loan not found");
        
        oldLoan.ApplicationStatus = request.ApplicationStatus;
        oldLoan.LoanAmount = request.LoanAmount;
        oldLoan.LoanTermInMonths = request.LoanTermInMonths;
        
        await _loanApplicationRepository.UpdateLoanApplicationAsync(oldLoan, cancellationToken: cancellationToken);
        var newLoan = _mapper.Map<LoanApplicationDto>(oldLoan);
        
        return ApiWrapper<UpdateLoanApplicationResult>.SuccessResponse(new UpdateLoanApplicationResult(newLoan));
    }
}