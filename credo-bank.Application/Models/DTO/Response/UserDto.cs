namespace credo_bank.Application.Models.DTO.Response;

public class UserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int IdentificationNumber { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<LoanApplicationDto>? LoanApplications { get; set; }
}