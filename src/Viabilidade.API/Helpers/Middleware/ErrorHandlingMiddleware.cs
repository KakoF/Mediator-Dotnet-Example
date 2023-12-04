using Newtonsoft.Json;
using System.Net;
using Viabilidade.Domain.Exceptions;

namespace Viabilidade.API.Helpers.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int code;
            ErrorResponse error;

            if (exception is DomainException)
            {
                var ex = (DomainException)exception;
                code = ex.StatusCode;
                error = new ErrorResponse(code, exception.Message);
            }
            else
            {
                code = (int)HttpStatusCode.InternalServerError;
                error = new ErrorResponse(code);
            }

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}