namespace credo_bank.Application.MediatR.User.DTO;

public class UserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int IdentificationNumber { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<LoanApplicationDto>? LoanApplications { get; set; }
}