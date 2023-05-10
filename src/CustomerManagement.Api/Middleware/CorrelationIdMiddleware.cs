using Azure.Core;

namespace CustomerManagement.Api.Middleware
{
    /// <summary>
    /// Custom middleware for populating a correlationId if not present on request
    /// </summary>
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeaderKey = "X-Correlation-ID";
        private readonly RequestDelegate _next;


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers.Add(CorrelationIdHeaderKey, correlationId);
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(CorrelationIdHeaderKey, correlationId);
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}