using AutoMapper;
using credo_bank.Application.Interfaces;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Add;

public class AddLoanApplicationHandler : IRequestHandler<AddLoanApplicationCommand, ApiWrapper<AddLoanApplicationResult>>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IMapper _mapper;
    public AddLoanApplicationHandler(ILoanApplicationRepository loanApplicationRepository, IMapper mapper)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _mapper = mapper;
    }

    public async Task<ApiWrapper<AddLoanApplicationResult>> Handle(AddLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        var loan = new Domain.Models.LoanApplication
        {
            UserId = request.UserId,
            LoanAmount = request.LoanAmount,
            LoanTermInMonths = request.LoanTermInMonths,
            CurrencyType = request.CurrencyType,
            ApplicationStatus = request.ApplicationStatus,
            LoanType = request.LoanType
        };
        
        var id = await _loanApplicationRepository.AddLoanApplicationAsync(loan, cancellationToken: cancellationToken);
        
        return ApiWrapper<AddLoanApplicationResult>.SuccessResponse(new AddLoanApplicationResult(id));
    }
}