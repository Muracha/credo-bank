using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Update;

public record UpdateLoanApplicationCommand(
    int LoanTermInMonths,
    decimal LoanAmount) : IRequest<ApiWrapper<UpdateLoanApplicationResult>>
{
    public int LoanId { get; set; }
}