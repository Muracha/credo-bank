using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplications;

public record GetLoanApplicationsQuery() : IRequest<ApiWrapper<GetLoanApplicationsResult>>;