using credo_bank.Application.MediatR.User.Commands.Register;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.Login;

public record LoginUserCommand: IRequest<ApiServiceResponse<LoginUserResult>>
{
    public int IdentificationNumber { get; set; }
    public string Password { get; set; }
}