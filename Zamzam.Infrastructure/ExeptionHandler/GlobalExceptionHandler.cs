using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Zamzam.Shared;

namespace Zamzam.Infrastructure.ExeptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);
        var details = new ProblemDetails()
        {
            Detail = $"Api Error : {exception.Message}",
            Instance = "API",
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "API Error",
            Type = "Server Error"
        };
        var resulException = Result<ProblemDetails>.Failure(details);
        var response = JsonSerializer.Serialize(resulException);
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(response);
        return true;
    }
}
