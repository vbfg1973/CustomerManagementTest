using System.Net;
using System.Text.Json;
using CustomerManagement.Common.Logging;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Middleware
{
    /// <summary>
    ///     Custom middleware for capturing unhandled exceptions and returning sensible error messages to the client
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private const string CorrelationIdHeaderKey = "X-Correlation-ID";
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "{Message} {CorrelationId}", LogFmt.Message("An unhandled exception has occurred: {ex.Message}"), LogFmt.CorrelationId(GetCorrelationId(context)));

            var result = string.Empty;

            if (ex is KeyNotFoundException)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Not found",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(problemDetails);
            }

            else
            {
                ProblemDetails problemDetails = new()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Instance = context.Request.Path,
                    Detail = "Internal server error occured!"
                };

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(problemDetails);
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }


        /// <summary>
        ///     Gets the correlation identifier.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out var correlationId))
                return correlationId;

            return string.Empty;
        }
    }
}