using credo_bank.Application.MediatR.User.Commands.Update;
using credo_bank.Application.MediatR.User.Queries.GetUserWithLoans;
using credo_bank.Application.Models.DTO.Request;
using credo_bank.Application.Utilities.ApiServiceResponse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace credo_bank.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : BaseController
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserWithLoans(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetUserWithLoansQuery(GetUserId()), cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordInputDto changePasswordInputDto, 
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new UpdateUserPasswordCommand(GetUserId(), 
            changePasswordInputDto.CurrentPassword, 
            changePasswordInputDto.NewPassword),
            cancellationToken: cancellationToken);
        
        return result.Success ? Ok(result) : BadRequest(result);
    }
}