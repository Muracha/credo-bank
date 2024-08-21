using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.Login;

public record LoginUserCommand(string IdentificationNumber, string Password) : IRequest<ApiWrapper<LoginUserResult>>;