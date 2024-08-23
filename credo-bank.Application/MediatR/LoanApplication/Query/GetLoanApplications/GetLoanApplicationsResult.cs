using credo_bank.Application.Models.DTO.Response;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplications;

public record GetLoanApplicationsResult(List<LoanApplicationDto> LoanApplicationDtos);