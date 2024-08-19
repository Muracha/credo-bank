using credo_bank.Application.Models.DTO;

namespace credo_bank.Application.MediatR.LoanApplication.Command.Update;

public record UpdateLoanApplicationResult(LoanApplicationDto LoanApplicationDto);