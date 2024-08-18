using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserWithLoans;

public record class GetUserWithLoansQuery : IRequest<ApiServiceResponse<GetUserWithLoansResult>>, IRequest<GetUserWithLoansResult>
{
    public int Id { get; set; }
}