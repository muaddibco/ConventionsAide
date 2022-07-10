using Microsoft.Extensions.DependencyInjection;

namespace ConventionsAide.Core.HealthChecks.ExtentionMethods
{
    public static class IHealthChecksBuilderExtention
    {
        public static IHealthChecksBuilder AddLogglyHealthCheck(this IHealthChecksBuilder builder)
        {
            builder.AddCheck<LogglyHealthCheck>("Loggly", tags: new[] { "logging", "loggly", "infra" });
            return builder;
        }

        public static IHealthChecksBuilder AddMassTransitHealthCheck(this IHealthChecksBuilder builder)
        {
            builder.AddCheck<MassTransitHealthCheck>("MassTransit", null, tags: new[] { "massTransit", "serviceBus", "infra" });
            return builder;
        }

        public static IHealthChecksBuilder AddBuildVersionHealthCheck(this IHealthChecksBuilder builder)
        {
            builder.AddCheck<BuildVersionHealthCheck>("BuildVersion", null, tags: new[] { "buildNumber", "devOps", "infra" });
            return builder;
        }
    }
}
