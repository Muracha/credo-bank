using credo_bank.Application.MediatR.User.DTO;

namespace credo_bank.Application.MediatR.User.Queries.GetUserById;

public record class GetUserByIdResult(UserDto UserDto);