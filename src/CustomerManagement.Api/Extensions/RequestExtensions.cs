namespace CustomerManagement.Api.Extensions
{
    /// <summary>
    ///     Helper extensions for HttpRequests
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        ///     Get correlation id from string if present
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetCorrelationId(this HttpRequest httpRequest)
        {
            if (httpRequest.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
                return correlationId!;

            return "CorrelationID not found";
        }
    }
}