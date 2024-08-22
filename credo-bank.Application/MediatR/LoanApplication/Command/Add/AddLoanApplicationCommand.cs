using System.ComponentModel.DataAnnotations;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.Domain.Enums;
using MediatR;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Add;

public record AddLoanApplicationCommand(
    decimal LoanAmount,
    int LoanTermInMonths,
    Currency CurrencyType,
    LoanType LoanType) : IRequest<ApiWrapper<AddLoanApplicationResult>>
{
    public int UserId { get; set; }
}