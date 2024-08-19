using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserWithLoans;

public record class GetUserWithLoansQuery(int Id) : IRequest<ApiWrapper<GetUserWithLoansResult>>;