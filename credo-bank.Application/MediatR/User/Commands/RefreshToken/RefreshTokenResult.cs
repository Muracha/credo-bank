using credo_bank.Application.Models.DTO;

namespace credo_bank.Application.MediatR.User.Commands.RefreshToken;

public record RefreshTokenResult(AuthReposnoseDto AuthReposnoseDto);