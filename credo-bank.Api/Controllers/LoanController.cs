using AutoMapper;
using credo_bank.Application.MediatR.LoanApplication.Command.Add;
using credo_bank.Application.MediatR.LoanApplication.Command.Approve;
using credo_bank.Application.MediatR.LoanApplication.Command.Delete;
using credo_bank.Application.MediatR.LoanApplication.Command.Reject;
using credo_bank.Application.MediatR.LoanApplication.Command.Update;
using credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationsByUserId;
using credo_bank.Application.Models.DTO.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace credo_bank.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LoanController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public LoanController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserWithLoans(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetLoanApplicationsByUserIdQuery(GetUserId()), cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    
    [HttpGet("loan/{id}")]
    public async Task<IActionResult> GetLoanById(int loanId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetLoanApplicationsByUserIdQuery(loanId), cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> ApplyForLoan([FromBody] ApplyForLoanInputDto applyForLoanInputDto, 
        CancellationToken cancellationToken = default)
    {
        var loan = _mapper.Map<AddLoanApplicationCommand>(applyForLoanInputDto);
        var result = await _mediator.Send(loan, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    
    [HttpPut("loan/{id}")]
    public async Task<IActionResult> UpdateLoan(int loanId, [FromBody] UpdateLoanDto applyForLoanInputDto, 
        CancellationToken cancellationToken = default)
    {
        var loan = _mapper.Map<UpdateLoanApplicationCommand>(applyForLoanInputDto);
        loan.LoanId = loanId;
        var result = await _mediator.Send(loan, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    
    [HttpDelete("loan/{id}")]
    public async Task<IActionResult> DeleteLoan(int loanId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new DeleteLoanApplicationCommand(loanId), cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
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
}