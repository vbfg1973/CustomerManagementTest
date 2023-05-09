using CustomerManagement.Api.Middleware;

namespace CustomerManagement.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}