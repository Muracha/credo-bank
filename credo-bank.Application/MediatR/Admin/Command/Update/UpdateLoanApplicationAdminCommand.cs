using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.Admin.Command.Update;

public record UpdateLoanApplicationAdminCommand(
    int LoanTermInMonths,
    decimal LoanAmount) : IRequest<ApiWrapper<UpdateLoanApplicationAdminResult>>
{
    public int LoanId { get; set; }
}