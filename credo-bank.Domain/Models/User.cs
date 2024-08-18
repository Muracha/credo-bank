namespace credo_bank.Domain.Models;

public class User : GenericEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int IdentificationNumber { get; set; }
    public string? Password { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<LoanApplication>? LoanApplications { get; set; }
}