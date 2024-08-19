using credo_bank.Application.MediatR.User.Models;
using credo_bank.Application.MediatR.User.Models.DTO;

namespace credo_bank.Application.MediatR.User.Queries.GetUserByIdentificationNumber;

public record class GetUserByIdnetificationNumberResult(UserDto UserDto);