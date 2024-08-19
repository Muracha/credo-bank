using credo_bank.Application.MediatR.User.Models;
using credo_bank.Application.MediatR.User.Models.DTO;

namespace credo_bank.Application.MediatR.User.Queries.GetUserById;

public record class GetUserByIdResult(UserDto UserDto);