namespace credo_bank.Application.Models.DTO.Request;

public record UpdateLoanDto(
    Domain.Enums.Application ApplicationStatus, 
    int LoanTermInMonths, 
    int LoanAmount);