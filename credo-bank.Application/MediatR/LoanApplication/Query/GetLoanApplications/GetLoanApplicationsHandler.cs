using AutoMapper;
using credo_bank.Application.Interfaces;
using credo_bank.Application.Models.DTO.Response;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplications;

public class GetLoanApplicationsHandler : IRequestHandler<GetLoanApplicationsQuery, ApiWrapper<GetLoanApplicationsResult>>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IMapper _mapper;
    public GetLoanApplicationsHandler(ILoanApplicationRepository loanApplicationRepository, IMapper mapper)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _mapper = mapper;
    }

    public async Task<ApiWrapper<GetLoanApplicationsResult>> Handle(GetLoanApplicationsQuery request, CancellationToken cancellationToken)
    {
        var loans = await _loanApplicationRepository.GetLoanApplicationsAsync(cancellationToken: cancellationToken);
        var loanDtos = _mapper.Map<List<LoanApplicationDto>>(loans);
        return ApiWrapper<GetLoanApplicationsResult>.SuccessResponse(new GetLoanApplicationsResult(loanDtos));
    }
}