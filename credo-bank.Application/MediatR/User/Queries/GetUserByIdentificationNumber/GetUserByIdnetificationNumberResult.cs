using credo_bank.Application.MediatR.User.DTO;

namespace credo_bank.Application.MediatR.User.Queries.GetUserByIdentificationNumber;

public record class GetUserByIdnetificationNumberResult(UserDto UserDto);