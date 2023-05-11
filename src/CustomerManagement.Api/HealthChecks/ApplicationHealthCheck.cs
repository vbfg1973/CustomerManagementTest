using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CustomerManagement.Api.HealthChecks
{
    public class ApplicationHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var assembly = Assembly.Load("QuotewareAdmin.Api");
            var versionNumber = assembly.GetName().Version;

            return Task.FromResult(HealthCheckResult.Healthy($"Build {versionNumber}"));
        }
    }
}