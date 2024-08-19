using credo_bank.Application.Models.DTO;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationsByUserId;

public record GetLoanApplicationsByUserIdResult(List<LoanApplicationDto> LoanApplications);