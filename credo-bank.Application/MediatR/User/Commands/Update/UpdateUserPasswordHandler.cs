using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.Update;

public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, ApiWrapper<UpdateUserPasswordResult>>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserPasswordHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ApiWrapper<UpdateUserPasswordResult>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken: cancellationToken);
        
        if (user == null)
            return ApiWrapper<UpdateUserPasswordResult>.FailureResponse("User not found");
        
        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
            return ApiWrapper<UpdateUserPasswordResult>.FailureResponse("Invalid current password");
        
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;
        
        var isUpdated = await _userRepository.UpdateUserAsync(user, cancellationToken: cancellationToken);
        
        return ApiWrapper<UpdateUserPasswordResult>.SuccessResponse(new UpdateUserPasswordResult(isUpdated), "Password updated successfully");
    }
}