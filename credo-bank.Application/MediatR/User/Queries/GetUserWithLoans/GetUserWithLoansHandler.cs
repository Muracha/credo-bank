using AutoMapper;
using credo_bank.Application.Models.DTO;
using credo_bank.Application.Utilities.ApiServiceResponse;
using credo_bank.DAL.Repositories.Interfaces;
using MediatR;

namespace credo_bank.Application.MediatR.User.Queries.GetUserWithLoans;

public class GetUserWithLoansHandler : IRequestHandler<GetUserWithLoansQuery, ApiWrapper<GetUserWithLoansResult>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public GetUserWithLoansHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ApiWrapper<GetUserWithLoansResult>> Handle(GetUserWithLoansQuery request, CancellationToken cancellationToken)
    {
        var userResult = await _repository.GetUserWithLoans(request.Id, cancellationToken: cancellationToken);
        var result = _mapper.Map<UserDto>(userResult);
        return userResult != null
            ? ApiWrapper<GetUserWithLoansResult>.SuccessResponse(new GetUserWithLoansResult(result))
            : ApiWrapper<GetUserWithLoansResult>.FailureResponse();
    }
}