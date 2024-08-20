using System.ComponentModel.DataAnnotations;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.Domain.Enums;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Add;

public record AddLoanApplicationCommand(
    int LoanAmount,
    int LoanTermInMonths,
    Currency CurrencyType,
    Domain.Enums.Application ApplicationStatus,
    LoanType LoanType) : IRequest<ApiWrapper<AddLoanApplicationResult>>
{
    public int UserId { get; set; }
}