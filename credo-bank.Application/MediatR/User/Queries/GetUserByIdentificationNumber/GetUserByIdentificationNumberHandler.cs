using AutoMapper;
using credo_bank.Application.MediatR.User.Models;
using credo_bank.Application.MediatR.User.Models.DTO;
using credo_bank.Application.MediatR.User.Queries.GetUserById;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserByIdentificationNumber;

public class GetUserByIdentificationNumberHandler : IRequestHandler<GetUserByIdentificationNumberQuery, ApiServiceResponse<GetUserByIdnetificationNumberResult>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public GetUserByIdentificationNumberHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ApiServiceResponse<GetUserByIdnetificationNumberResult>> Handle(GetUserByIdentificationNumberQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _repository.GetUserByIdentificationNumber(request.IdentificationNumber, cancellationToken: cancellationToken);
        var result = _mapper.Map<UserDto>(userResult);
        return userResult != null
            ? ApiServiceResponse<GetUserByIdnetificationNumberResult>.SuccessResponse(new GetUserByIdnetificationNumberResult(result))
            : ApiServiceResponse<GetUserByIdnetificationNumberResult>.FailureResponse();
    }
}