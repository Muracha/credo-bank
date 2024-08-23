using AutoMapper;
using credo_bank.Application.Interfaces;
using credo_bank.Application.Models.DTO.Response;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;

namespace credo_bank.Application.MediatR.Role.Query.GetRoles;

public class GetRolesHandler : IRequestHandler<GetRolesQuery, ApiWrapper<GetRolesResult>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    public GetRolesHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<ApiWrapper<GetRolesResult>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken: cancellationToken);
        var roleDtos = _mapper.Map<List<RoleDto>>(roles);
        return ApiWrapper<GetRolesResult>.SuccessResponse(new GetRolesResult(roleDtos));
    }
}