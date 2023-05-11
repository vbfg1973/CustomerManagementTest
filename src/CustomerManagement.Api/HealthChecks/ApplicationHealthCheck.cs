using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CustomerManagement.Api.HealthChecks
{
    /// <summary>
    ///     Application health check
    /// </summary>
    public class ApplicationHealthCheck : IHealthCheck
    {
        /// <summary>
        ///     The health check
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var assembly = Assembly.Load("CustomerManagement.Api");
            var versionNumber = assembly.GetName().Version;

            return Task.FromResult(HealthCheckResult.Healthy($"Build {versionNumber}"));
        }
    }
}