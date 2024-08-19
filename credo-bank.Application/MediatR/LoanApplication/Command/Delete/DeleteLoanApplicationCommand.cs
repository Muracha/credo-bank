using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Delete;

public record DeleteLoanApplicationCommand(int LoanId) : IRequest<ApiWrapper<DeleteLoanApplicationResult>>;