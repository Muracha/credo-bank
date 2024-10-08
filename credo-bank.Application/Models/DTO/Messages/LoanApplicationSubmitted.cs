﻿using credo_bank.Domain.Enums;

namespace credo_bank.Application.Models.DTO.Messages;

public class LoanApplicationSubmitted
{
    public int LoanId { get; set; }
    public int UserId { get; set; }
    public decimal LoanAmount { get; set; }
    public int LoanTermInMonths { get; set; }
    public Currency CurrencyType { get; set; }
    public Domain.Enums.Application ApplicationStatus { get; set; }
    public LoanType LoanType { get; set; }
    public DateTime CreateDate { get; set; }
}