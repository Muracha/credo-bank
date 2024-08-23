using System.Security.Claims;
using System.Text;
using Serilog;

namespace credo_bank.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;
        
        var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        int.TryParse(idClaim?.Value, out var id);
        
        var userId = context.User.Identity.IsAuthenticated ? id.ToString() : "-1";
        var endpoint = context.Request.Path;

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        Log.Information("Request: {RequestBody}, Endpoint: {Endpoint}, Date: {Date}, UserId: {UserId}, Response: {ResponseBody}",
            requestBody, endpoint, DateTime.Now, userId, responseBodyText);

        await responseBody.CopyToAsync(originalBodyStream);
    }
}