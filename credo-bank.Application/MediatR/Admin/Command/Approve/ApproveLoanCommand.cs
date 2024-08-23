using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.Admin.Command.Approve;

public record ApproveLoanCommand(int LoanId) : IRequest<ApiWrapper<ApproveLoanResult>>;