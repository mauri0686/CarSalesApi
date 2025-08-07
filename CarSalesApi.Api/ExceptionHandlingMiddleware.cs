using System.Net;
using System.Text.Json;

namespace CarSalesApi.Api
{
    /// <summary>
    /// Middleware for handling unhandled exceptions during HTTP request processing in the middleware pipeline.
    /// </summary>
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        /// <summary>
        /// Processes an incoming HTTP request and handles any unhandled exceptions that occur
        /// during the middleware pipeline.
        /// </summary>
        /// <param name="context">The HttpContext for the current request.</param>
        /// <returns>A task that represents the completion of the middleware pipeline execution.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    message = ex.Message
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}