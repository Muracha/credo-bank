using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationsByUserId;

public record GetLoanApplicationsByUserIdQuery(int UserId) : IRequest<ApiWrapper<GetLoanApplicationsByUserIdResult>>;