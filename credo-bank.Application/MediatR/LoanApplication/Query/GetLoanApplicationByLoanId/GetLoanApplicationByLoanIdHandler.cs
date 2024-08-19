using AutoMapper;
using credo_bank.Application.Models.DTO;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationByLoanId;

public class GetLoanApplicationByLoanIdHandler : IRequestHandler<GetLoanApplicationByLoanIdQuery, ApiWrapper<GetLoanApplicationByLoanIdResult>>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IMapper _mapper;
    public GetLoanApplicationByLoanIdHandler(ILoanApplicationRepository loanApplicationRepository, IMapper mapper)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _mapper = mapper;
    }
    
    public async Task<ApiWrapper<GetLoanApplicationByLoanIdResult>> Handle(GetLoanApplicationByLoanIdQuery request, CancellationToken cancellationToken)
    {
        var loanApplication = await _loanApplicationRepository.GetLoanWithId(request.LoanId, cancellationToken: cancellationToken);
        
        var result = _mapper.Map<LoanApplicationDto>(loanApplication);
        
        return ApiWrapper<GetLoanApplicationByLoanIdResult>.SuccessResponse(new GetLoanApplicationByLoanIdResult(result));
    }
}