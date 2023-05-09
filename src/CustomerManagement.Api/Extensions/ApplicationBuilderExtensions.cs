using CustomerManagement.Api.Middleware;

namespace CustomerManagement.Api.Extensions
{
    /// <summary>
    ///     Helper extensions for configuring application builder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///     Adds custom exception handling support
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        /// <summary>
        ///     Adds support for correlation ids where not provided by the client
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}