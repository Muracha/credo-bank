using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.Register;

public record RegisterUserCommand : IRequest<ApiServiceResponse<RegisterUserResult>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int IdentificationNumber { get; set; }
    public string? Password { get; set; }
    public DateTime DateOfBirth { get; set; }
}