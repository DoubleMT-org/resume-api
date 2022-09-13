using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Resume.Service.Exceptions;
using System.Threading.Tasks;

namespace Resume.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (EventException ex)
            {
                httpContext.Response.StatusCode = ex.Code;

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    ex.Code,
                    ex.Message
                });
            }
            catch (Exception ex)
            {
                // logger to here

                httpContext.Response.StatusCode = 500;

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Code = 500,
                    ex.Message
                });
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionsHendlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionsHendlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsHandlerMiddleware>();
        }
    }
}
