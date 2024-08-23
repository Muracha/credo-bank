using System.ComponentModel.DataAnnotations;

namespace credo_bank.Domain.Models;

public class User : GenericEntity
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? LastName { get; set; }
    
    [Required]
    [StringLength(11, MinimumLength = 11)]
    public string IdentificationNumber { get; set; }
    
    [Required]
    [StringLength(255)]
    public string? Password { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
    public ICollection<LoanApplication>? LoanApplications { get; set; }
}