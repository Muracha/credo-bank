using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Update;

public record UpdateLoanApplicationCommand(
    Domain.Enums.Application ApplicationStatus,
    int LoanTermInMonths,
    int LoanAmount) : IRequest<ApiWrapper<UpdateLoanApplicationResult>>
{
    public int LoanId { get; set; }
}