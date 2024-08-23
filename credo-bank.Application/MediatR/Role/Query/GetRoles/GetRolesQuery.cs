using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.Role.Query.GetRoles;

public record GetRolesQuery : IRequest<ApiWrapper<GetRolesResult>>;