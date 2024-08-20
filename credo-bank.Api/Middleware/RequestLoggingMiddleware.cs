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
        var requestBodyContent = await ReadRequestBodyAsync(context.Request);
        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next(context);

            var responseBodyContent = await ReadResponseBodyAsync(context.Response);
            await responseBody.CopyToAsync(originalBodyStream);

            LogRequestAndResponse(context, requestBodyContent, responseBodyContent);
        }
    }

    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.Body.Position = 0;
        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var content = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return content;
        }
    }

    private async Task<string> ReadResponseBodyAsync(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return text;
    }

    private void LogRequestAndResponse(HttpContext context, string requestBody, string responseBody)
    {
        var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

        int id;
        if (!int.TryParse(idClaim?.Value, out id))
        {
            id = -1;
        }

        var emailClaim = context.User.FindFirst(ClaimTypes.Email);
        var email = emailClaim?.Value ?? string.Empty;

        Log.Information("Credo - Request {Method} {Url} => {StatusCode} \nRequest Body: {RequestBody} \nResponse Body: {ResponseBody} \nUserId = {UserId}",
            context.Request?.Method,
            context.Request?.Path.Value,
            context.Response?.StatusCode,
            requestBody,
            responseBody,
            id);
    }
}