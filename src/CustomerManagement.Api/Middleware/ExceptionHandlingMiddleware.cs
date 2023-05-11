using System.Net;
using System.Text.Json;
using CustomerManagement.Common.Logging;
using CustomerManagement.Domain.Exceptions;
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
            var result = ex switch
            {
                ResourceNotFoundException => ResourceNotFoundHandler(context, ex),
                KeyNotFoundException => KeyNotFoundHandler(context, ex),
                _ => DefaultHandler(context, ex)
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }

        private string DefaultHandler(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "{Message} {CorrelationId}",
                LogFmt.Message("An unhandled exception has occurred: {ex.Message}"),
                LogFmt.CorrelationId(GetCorrelationId(context)));

            ProblemDetails problemDetails = new()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error",
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path,
                Detail = "Internal server error occured!"
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return JsonSerializer.Serialize(problemDetails);
        }

        private string KeyNotFoundHandler(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "{Message} {CorrelationId}",
                LogFmt.Message("An unhandled exception has occurred: {ex.Message}"),
                LogFmt.CorrelationId(GetCorrelationId(context)));

            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Not found",
                Status = (int)HttpStatusCode.NotFound,
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return JsonSerializer.Serialize(problemDetails);
        }

        private string ResourceNotFoundHandler(HttpContext context, Exception ex)
        {
            _logger.LogWarning("{Message} {CorrelationId}",
                LogFmt.Message("The requested resource was not found"),
                LogFmt.CorrelationId(GetCorrelationId(context)));

            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Not found",
                Status = (int)HttpStatusCode.NotFound,
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return JsonSerializer.Serialize(problemDetails);
        }


        /// <summary>
        ///     Gets the correlation identifier.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out var correlationId))
                return correlationId;

            return string.Empty;
        }
    }
}