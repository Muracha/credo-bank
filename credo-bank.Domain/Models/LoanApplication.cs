using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using credo_bank.Domain.Enums;

namespace credo_bank.Domain.Models;

public class LoanApplication : GenericEntity
{
    [ForeignKey("User")]
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public decimal LoanAmount { get; set; }
    
    [Required]
    public int LoanTermInMonths { get; set; }
    
    [Required]
    public Currency CurrencyType { get; set; }
    
    [Required]
    public Application ApplicationStatus { get; set; }
    
    [Required]
    public LoanType LoanType { get; set; }

    public bool IsDeleted { get; set; } = false;
    
    public DateTime CreateDate { get; set; } = DateTime.Now;
    
    public User? User { get; set; }
}