using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.Update;

public record UpdateUserPasswordCommand(
    int UserId, 
    string CurrentPassword, 
    string NewPassword) : IRequest<ApiWrapper<UpdateUserPasswordResult>>;