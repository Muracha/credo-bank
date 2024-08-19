using credo_bank.Application.MediatR.User.Models;
using credo_bank.Application.MediatR.User.Models.DTO;

namespace credo_bank.Application.MediatR.User.Queries.GetUserWithLoans;

public record class GetUserWithLoansResult(UserDto UserDto);