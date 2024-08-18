using AutoMapper;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;

namespace credo_bank.Application.MediatR.User.Commands.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ApiServiceResponse<RegisterUserResult>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public RegisterUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ApiServiceResponse<RegisterUserResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetUserByIdentificationNumber(request.IdentificationNumber, cancellationToken);
        
        if (existingUser != null)
            return ApiServiceResponse<RegisterUserResult>.FailureResponse();
        
        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var userToBeCreated = _mapper.Map<Domain.Models.User>(request);
        await _repository.AddUserAsync(userToBeCreated);

        return userToBeCreated.Id != 0
            ? ApiServiceResponse<RegisterUserResult>.SuccessResponse(
                new RegisterUserResult(true, "User registered successfully.", userToBeCreated.Id))
            : ApiServiceResponse<RegisterUserResult>.FailureResponse();
    }
}