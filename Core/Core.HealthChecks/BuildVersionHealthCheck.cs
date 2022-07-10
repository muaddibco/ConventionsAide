using ConventionsAide.Core.Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConventionsAide.Core.HealthChecks
{
    public class BuildVersionHealthCheck : IHealthCheck
    {

        public BuildVersionHealthCheck()
        {
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                string buildVersion = $"{Environment.GetEnvironmentVariable(EnvironmentConsts.BuildNumberEnvName)}" +
                        $"_{Environment.GetEnvironmentVariable(EnvironmentConsts.BuildDateEnvName)}";

                return HealthCheckResult.Healthy(buildVersion);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message, ex);
            }
        }
    }
}
