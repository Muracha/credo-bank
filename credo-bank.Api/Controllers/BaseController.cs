using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace credo_bank.Controllers;

public class BaseController : ControllerBase
{
    protected int GetUserId()
    {
        var httpContext = HttpContext;
        var idClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        int.TryParse(idClaim?.Value, out var id);
        return id;
    }
}