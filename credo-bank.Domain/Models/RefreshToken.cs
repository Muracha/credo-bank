using System.ComponentModel.DataAnnotations;

namespace credo_bank.Domain.Models;

public class RefreshToken : GenericEntity
{
    [Required]
    [StringLength(500)]
    public string Token { get; set; }
        
    [Required]
    [StringLength(50)]
    public string JwtId { get; set; }
        
    [Required]
    public DateTime CreatedDate { get; set; }
        
    [Required]
    public DateTime ExpirationDate { get; set; }
        
    [Required]
    public bool Invalidated { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public User User { get; set; }
    
    public void InvalidateToken()
    {
        Invalidated = true;
    }
}