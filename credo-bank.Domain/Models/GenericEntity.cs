using System.ComponentModel.DataAnnotations;

namespace credo_bank.Domain.Models;

public class GenericEntity
{
    [Key]
    public int Id { get; set; }
}