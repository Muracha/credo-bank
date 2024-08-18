using credo_bank.Domain.Enums;

namespace credo_bank.Domain.Models;

public class LoanApplication : GenericEntity
{
    public int LoanAmount { get; set; }
    public int LoanTermInMonths { get; set; }
    public Currency CurrencyType { get; set; }
    public Application ApplicationStatus { get; set; }
    public LoanType LoanType { get; set; }
    public DateTime CreateDate { get; set; }
    
    public User? User { get; set; }
}