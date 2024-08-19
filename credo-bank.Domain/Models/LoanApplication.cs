using System.ComponentModel.DataAnnotations;
using credo_bank.Domain.Enums;

namespace credo_bank.Domain.Models;

public class LoanApplication : GenericEntity
{
    [Required]
    public int LoanAmount { get; set; }
    
    [Required]
    public int LoanTermInMonths { get; set; }
    
    [Required]
    public Currency CurrencyType { get; set; }
    
    [Required]
    public Application ApplicationStatus { get; set; }
    
    [Required]
    public LoanType LoanType { get; set; }
    
    public DateTime CreateDate { get; set; } = DateTime.Now;
    
    public User? User { get; set; }
}