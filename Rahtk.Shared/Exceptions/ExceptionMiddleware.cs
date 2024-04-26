using Microsoft.AspNetCore.Http;
using Rahtk.Shared.Models;

namespace Rahtk.Shared.Exceptions
{
	public class ExceptionMiddleware : IMiddleware
	{
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 400;
                context.Response.Headers.Add("content-type", "application/json");
                
                var json = new BaseResponse<String> { statusCode = 400, message = "error" }.ToString();
                await context.Response.WriteAsync(json);
            }
        }
    }
}