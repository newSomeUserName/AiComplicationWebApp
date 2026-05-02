using Microsoft.AspNetCore.Diagnostics;
using UnifiedAIChat.Application.Common.Exceptions;

namespace UnifiedAIChat.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            var exceptionStatus = exception switch
            {
                ConflictException => new { Code = StatusCodes.Status409Conflict, Message = exception.Message },
                _ => new { Code = StatusCodes.Status500InternalServerError, Message = exception.Message },
            };


            httpContext.Response.StatusCode = exceptionStatus.Code;
            await httpContext.Response.WriteAsync(exceptionStatus.Message,cancellationToken);

            _logger.LogWarning($"{exceptionStatus.Code}:    \n {exceptionStatus.Message}");

            return true;
        }
    }
}
