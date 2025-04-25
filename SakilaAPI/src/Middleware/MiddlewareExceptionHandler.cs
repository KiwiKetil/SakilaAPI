
using System.Runtime.CompilerServices;

namespace SakilaAPI.Middleware;

public class MiddlewareExceptionHandler : IMiddleware
{
    private readonly ILogger<MiddlewareExceptionHandler> _logger;

    public MiddlewareExceptionHandler(ILogger<MiddlewareExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Something went wrong - test exception {@Machine} {@TraceId}",
                Environment.MachineName,
                System.Diagnostics.Activity.Current?.Id);

            await Results.Problem(
                title: "Something unexpected has happened! Please contact support if the problem persists.",
                statusCode: StatusCodes.Status500InternalServerError,
                extensions: new Dictionary<string, object?>
                {
                    { "traceId", System.Diagnostics.Activity.Current?.Id },
                })
                .ExecuteAsync(context);
        }
    }
} 