using AutoMapper;
using credo_bank.Application.MediatR.Admin.Command.Approve;
using credo_bank.Application.MediatR.Admin.Command.Delete;
using credo_bank.Application.MediatR.Admin.Command.Reject;
using credo_bank.Application.MediatR.LoanApplication.Command.Add;
using credo_bank.Application.MediatR.LoanApplication.Command.Update;
using credo_bank.Application.MediatR.LoanApplication.Query.GetLoanApplicationByLoanId;
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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoanById(int id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetLoanApplicationByLoanIdQuery(id), cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> ApplyForLoan([FromBody] ApplyForLoanInputDto applyForLoanInputDto, 
        CancellationToken cancellationToken = default)
    {
        var loan = _mapper.Map<AddLoanApplicationCommand>(applyForLoanInputDto);
        loan.UserId = GetUserId();
        var result = await _mediator.Send(loan, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanDto applyForLoanInputDto, 
        CancellationToken cancellationToken = default)
    {
        var loan = _mapper.Map<UpdateLoanApplicationCommand>(applyForLoanInputDto);
        loan.LoanId = id;
        var result = await _mediator.Send(loan, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}