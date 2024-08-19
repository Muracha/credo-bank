using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<ApiServiceResponse<RefreshTokenResult>>
{
    public string Token { get; set; }
}