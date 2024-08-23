using AutoMapper;
using credo_bank.Application.MediatR.Admin.Command.Approve;
using credo_bank.Application.MediatR.Admin.Command.Reject;
using credo_bank.Application.MediatR.Admin.Command.Update;
using credo_bank.Application.MediatR.LoanApplication.Command.Update;
using credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplications;
using credo_bank.Application.Models.DTO.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace credo_bank.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public AdminController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllLoan(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetLoanApplicationsQuery(), cancellationToken: cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("approve/{id}")]
    public async Task<IActionResult> ApproveLoan(int id)
    {
        var result = await _mediator.Send(new ApproveLoanCommand(id));
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    [HttpPost("reject/{id}")]
    public async Task<IActionResult> RejectLoan(int id)
    {
        var result = await _mediator.Send(new RejectLoanCommand(id));
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanDto applyForLoanInputDto, 
        CancellationToken cancellationToken = default)
    {
        var loan = _mapper.Map<UpdateLoanApplicationAdminCommand>(applyForLoanInputDto);
        loan.LoanId = id;
        var result = await _mediator.Send(loan, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}