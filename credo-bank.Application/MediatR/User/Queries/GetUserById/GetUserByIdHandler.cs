using AutoMapper;
using credo_bank.Application.MediatR.User.DTO;
using credo_bank.Application.MediatR.User.Queries.GetUserById;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUser;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ApiServiceResponse<GetUserByIdResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<ApiServiceResponse<GetUserByIdResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken: cancellationToken);
        var result = _mapper.Map<UserDto>(userResult);
        return userResult != null
            ? ApiServiceResponse<GetUserByIdResult>.SuccessResponse(new GetUserByIdResult(result))
            : ApiServiceResponse<GetUserByIdResult>.FailureResponse();
    }
}