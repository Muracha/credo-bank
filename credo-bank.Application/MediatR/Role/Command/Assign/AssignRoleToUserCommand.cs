using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.Role.Command.Assign;

public record AssignRoleToUserCommand(int RoleId, int UserId) : IRequest<ApiWrapper<AssignRoleToUserResult>>;