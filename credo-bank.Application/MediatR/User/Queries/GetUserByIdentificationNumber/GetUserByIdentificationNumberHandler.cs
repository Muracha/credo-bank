using AutoMapper;
using credo_bank.Application.Interfaces;
using credo_bank.Application.Models.DTO;
using credo_bank.Application.Models.DTO.Response;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserByIdentificationNumber;

public class GetUserByIdentificationNumberHandler : IRequestHandler<GetUserByIdentificationNumberQuery, ApiWrapper<GetUserByIdnetificationNumberResult>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public GetUserByIdentificationNumberHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ApiWrapper<GetUserByIdnetificationNumberResult>> Handle(GetUserByIdentificationNumberQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _repository.GetUserByIdentificationNumber(request.IdentificationNumber, cancellationToken: cancellationToken);
        var result = _mapper.Map<UserDto>(userResult);
        return userResult != null
            ? ApiWrapper<GetUserByIdnetificationNumberResult>.SuccessResponse(new GetUserByIdnetificationNumberResult(result))
            : ApiWrapper<GetUserByIdnetificationNumberResult>.FailureResponse();
    }
}