namespace credo_bank.Application.Models.DTO.Request;

public record UpdateLoanDto(
    int LoanTermInMonths, 
    decimal LoanAmount);