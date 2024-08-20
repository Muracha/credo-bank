using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Response;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationByLoanId;

public record GetLoanApplicationByLoanIdResult(LoanApplicationDto LoanApplicationDto);