using System.ComponentModel.DataAnnotations;

namespace credo_bank.Domain.Models;

public class Role : GenericEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}