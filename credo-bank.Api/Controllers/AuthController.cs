using AutoMapper;
using credo_bank.Application.MediatR.User.Commands.Login;
using credo_bank.Application.MediatR.User.Commands.RefreshToken;
using credo_bank.Application.MediatR.User.Commands.Register;
using credo_bank.Application.Models.DTO.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace credo_bank.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserInputDto registerUserInputDto, 
        CancellationToken cancellationToken = default)
    {
        var register = _mapper.Map<RegisterUserCommand>(registerUserInputDto);
        var result = await _mediator.Send(register , cancellationToken: cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginInputDto loginInputDto, 
        CancellationToken cancellationToken = default)
    {
        var login = _mapper.Map<LoginUserCommand>(loginInputDto);
        var result = await _mediator.Send(login, cancellationToken: cancellationToken);
        return result.Success ? Ok(result) : Unauthorized(result);
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenInputDto refreshTokenInputDto,
        CancellationToken cancellationToken = default)
    {
        var refreshToken = _mapper.Map<RefreshTokenCommand>(refreshTokenInputDto);
        var result = await _mediator.Send(refreshToken, cancellationToken: cancellationToken);
        return result.Success ? Ok(result) : Unauthorized(result);
    }
}