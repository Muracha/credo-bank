using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.Login;

public record LoginUserCommand(int IdentificationNumber, string Password) : IRequest<ApiWrapper<LoginUserResult>>;