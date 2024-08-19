using AutoMapper;
using credo_bank.Application.Models.DTO;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationsByUserId;

public class GetLoanApplicationsByUserIdHandler : IRequestHandler<GetLoanApplicationsByUserIdQuery, ApiWrapper<GetLoanApplicationsByUserIdResult>>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IMapper _mapper;
    public GetLoanApplicationsByUserIdHandler(ILoanApplicationRepository loanApplicationRepository, IMapper mapper)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _mapper = mapper;
    }

    public async Task<ApiWrapper<GetLoanApplicationsByUserIdResult>> Handle(GetLoanApplicationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var loanApplications = await _loanApplicationRepository.GetLoanApplicationsByUserIdAsync(request.UserId, cancellationToken: cancellationToken);
        
        var result = _mapper.Map<List<LoanApplicationDto>>(loanApplications);
        
        return ApiWrapper<GetLoanApplicationsByUserIdResult>.SuccessResponse(new GetLoanApplicationsByUserIdResult(result));
    }
}