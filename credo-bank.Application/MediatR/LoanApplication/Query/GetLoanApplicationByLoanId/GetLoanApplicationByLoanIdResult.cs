﻿using credo_bank.Application.Models.DTO;

namespace credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationByLoanId;

public record GetLoanApplicationByLoanIdResult(LoanApplicationDto LoanApplicationDto);