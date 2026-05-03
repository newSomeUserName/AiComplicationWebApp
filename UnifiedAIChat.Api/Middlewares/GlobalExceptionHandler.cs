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

            #region RawRealization
            //var exceptionStatus = exception switch
            //{
            //    ConflictException => new { Code = StatusCodes.Status409Conflict, Message = exception.Message },
            //    _ => new { Code = StatusCodes.Status500InternalServerError, Message = exception.Message },
            //};


            //httpContext.Response.StatusCode = exceptionStatus.Code;
            //await httpContext.Response.WriteAsync(exceptionStatus.Message, cancellationToken); 
            #endregion

            var exceptionStatus = _returnCodeInfo(exception);

            if (exceptionStatus.ExceptionCode >= 500)
            {
                _logger.LogError($"{exceptionStatus.ExceptionCode}:    \n {exceptionStatus.Message}");
            }
            else
            {
                _logger.LogWarning($"{exceptionStatus.ExceptionCode}:    \n {exceptionStatus.Message}");
            }

            //_logger.LogWarning($"{exceptionStatus.ExceptionCode}:    \n {exceptionStatus.Message}");

            
            httpContext.Response.StatusCode = exceptionStatus.ExceptionCode;
            await httpContext.Response.WriteAsync(exceptionStatus.Message, cancellationToken);


            return true;
        }
        private (int ExceptionCode, string Message, LogLevel logLevel) _returnCodeInfo(Exception exception)
        {

            return exception switch
            {
                ConflictException => ( StatusCodes.Status409Conflict,exception.Message,LogLevel.Warning),
                _ => (StatusCodes.Status500InternalServerError,exception.Message, LogLevel.Error)
            };
        }
    }
}
