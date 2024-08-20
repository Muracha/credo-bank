using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Response;

namespace credo_bank.Application.MediatR.User.Queries.GetUserWithLoans;

public record class GetUserWithLoansResult(UserDto UserDto);