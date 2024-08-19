using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.RefreshToken;

public record RefreshTokenCommand(string Token) : IRequest<ApiWrapper<RefreshTokenResult>>;