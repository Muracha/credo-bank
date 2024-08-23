using credo_bank.Application.MediatR.Role.Command.Assign;
using credo_bank.Application.MediatR.Role.Query.GetRoles;
using credo_bank.Application.Models.DTO.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace credo_bank.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class RoleController : BaseController
{
    private readonly IMediator _mediator;
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetRolesQuery(), cancellationToken: cancellationToken);
        return Ok(result);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleInputDto assignRoleInputDto, 
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new AssignRoleToUserCommand(assignRoleInputDto.RoleId, assignRoleInputDto.UserId), cancellationToken: cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}