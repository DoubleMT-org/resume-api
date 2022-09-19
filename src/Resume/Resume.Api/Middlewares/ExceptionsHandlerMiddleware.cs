using Resume.Service.Exceptions;

namespace Resume.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsHandlerMiddleware> _logger;

        public ExceptionsHandlerMiddleware(RequestDelegate next, ILogger<ExceptionsHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                _logger.LogError(message: ex.ToString());

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
