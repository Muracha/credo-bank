using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserById;

public record class GetUserByIdQuery(int Id) : IRequest<ApiWrapper<GetUserByIdResult>>;