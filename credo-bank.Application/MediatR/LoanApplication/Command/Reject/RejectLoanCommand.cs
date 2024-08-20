using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Reject;

public record RejectLoanCommand(int LoanId) : IRequest<ApiWrapper<RejectLoanResult>>;