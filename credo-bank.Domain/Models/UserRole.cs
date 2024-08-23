using System.ComponentModel.DataAnnotations;

namespace credo_bank.Domain.Models;

public class UserRole : GenericEntity
{
    [Required]
    public int UserId { get; set; }
        
    [Required]
    public User User { get; set; }

    [Required]
    public int RoleId { get; set; }
        
    [Required]
    public Role Role { get; set; }
}