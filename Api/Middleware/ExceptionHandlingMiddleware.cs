using Application.Exceptions;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await HandleExceptionAsync(httpContext, ex, _env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            string title = "An unexpected error occurred.";
            string? detail = exception.Message;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    title = exception.Message;
                    break;
                case Application.Exceptions.BadRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = exception.Message;
                    break;
                default:
                    // leave as 500
                    break;
            }

            // In development, include inner exception details for easier debugging
            if (env.IsDevelopment() && exception.InnerException != null)
            {
                detail += " | Inner: " + exception.InnerException.Message;
            }

            var problem = new
            {
                type = "about:blank",
                title,
                status = statusCode,
                detail
            };

            var payload = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}
