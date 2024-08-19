using credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationsByUserId;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationByLoanId;

public record GetLoanApplicationByLoanIdQuery(int LoanId) : IRequest<ApiWrapper<GetLoanApplicationByLoanIdResult>>;