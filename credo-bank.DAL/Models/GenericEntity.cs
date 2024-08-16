using System.ComponentModel.DataAnnotations;

namespace credo_bank.DAL.Models;

public class GenericEntity
{
    [Key]
    public int Id { get; set; }
}