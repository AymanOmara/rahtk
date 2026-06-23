using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rahtk.Shared.Models;

namespace Rahtk.Shared.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception occurred during request execution: {Message}", ex.Message);

                context.Response.StatusCode = 400;
                context.Response.Headers.Append("content-type", "application/json");

                var json = new BaseResponse<string>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Success = false
                }.ToString();

                await context.Response.WriteAsync(json);
            }
        }
    }
}