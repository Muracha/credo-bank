using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.Register;

public record RegisterUserCommand(
    string FirstName, 
    string LastName, 
    string IdentificationNumber, 
    string Password, 
    DateTime DateOfBirth) : IRequest<ApiWrapper<RegisterUserResult>>;