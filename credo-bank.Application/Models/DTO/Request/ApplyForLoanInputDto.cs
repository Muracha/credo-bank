using credo_bank.Domain.Enums;

namespace credo_bank.Application.Models.DTO.Request;

public record ApplyForLoanInputDto(
    int UserId, 
    decimal LoanAmount, 
    int LoanTermInMonths, 
    Currency CurrencyType,
    Domain.Enums.Application ApplicationStatus,
    LoanType LoanType);