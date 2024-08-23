using credo_bank.Application.Models.DTO.Response;

namespace credo_bank.Application.MediatR.Role.Query.GetRoles;

public record GetRolesResult(List<RoleDto> Roles);