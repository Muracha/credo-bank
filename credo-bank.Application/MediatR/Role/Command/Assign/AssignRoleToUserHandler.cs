using credo_bank.Application.Interfaces;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.Domain.Models;
using MediatR;

namespace credo_bank.Application.MediatR.Role.Command.Assign;

public class AssignRoleToUserHandler : IRequestHandler<AssignRoleToUserCommand, ApiWrapper<AssignRoleToUserResult>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    public AssignRoleToUserHandler(IRoleRepository roleRepository, IUserRepository userRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async Task<ApiWrapper<AssignRoleToUserResult>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdWithRoleWithTrackingAsync(request.UserId, cancellationToken: cancellationToken);
        if (user is null)
            return ApiWrapper<AssignRoleToUserResult>.FailureResponse("User not found");
        
        var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken: cancellationToken);
        if (role is null)
            return ApiWrapper<AssignRoleToUserResult>.FailureResponse("Role not found");
        
        var existingUserRole = user.UserRoles?.FirstOrDefault(ur => ur.UserId == request.UserId);
    
        if (existingUserRole is not null)
            existingUserRole.RoleId = role.Id;
        else
        {
            var userRole = new UserRole { UserId = request.UserId, RoleId = role.Id };
            user.UserRoles?.Add(userRole);
        }

        var wasSuccess = await _userRepository.SaveChangesAsync(cancellationToken);

        return ApiWrapper<AssignRoleToUserResult>.SuccessResponse(new AssignRoleToUserResult(wasSuccess), "Role assignment updated successfully");
    }
}