using credo_bank.Application.Models.DTO;

namespace credo_bank.Application.MediatR.User.Queries.GetUserWithLoans;

public record class GetUserWithLoansResult(UserDto UserDto);