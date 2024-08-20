using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Response;

namespace credo_bank.Application.MediatR.User.Commands.Login;

public record LoginUserResult(AuthReposnoseDto AuthReposnoseDto);