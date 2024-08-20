using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Response;

namespace credo_bank.Application.MediatR.User.Commands.RefreshToken;

public record RefreshTokenResult(AuthReposnoseDto AuthReposnoseDto);