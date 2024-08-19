using credo_bank.Domain.Enums;

namespace credo_bank.Application.MediatR.User.Models.DTO;

public class LoanApplicationDto
{
    public int LoanAmount { get; set; }
    public int LoanTermInMonths { get; set; }
    public Currency CurrencyType { get; set; }
    public Domain.Enums.Application ApplicationStatus { get; set; }
    public LoanType LoanType { get; set; }
    public DateTime CreateDate { get; set; }
}