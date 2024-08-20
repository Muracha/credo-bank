using AutoMapper;
using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Response;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ApiWrapper<GetUserByIdResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<ApiWrapper<GetUserByIdResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken: cancellationToken);
        var result = _mapper.Map<UserDto>(userResult);
        return userResult != null
            ? ApiWrapper<GetUserByIdResult>.SuccessResponse(new GetUserByIdResult(result))
            : ApiWrapper<GetUserByIdResult>.FailureResponse();
    }
}