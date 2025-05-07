using System.Net;
using Contracts;
using Entities.ErrorModels;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace PulseWorkout;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILoggerManager _logger;
    public GlobalExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            httpContext.Response.StatusCode = contextFeature.Error switch
            {
                NotFoundException => StatusCodes.Status404NotFound, 
                BadRequestException => StatusCodes.Status400BadRequest,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            _logger.LogError($"Something went wrong: {exception.Message}");
            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = contextFeature.Error.Message,
            }.ToString());
        }
        return true;
    }
}
