using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ConventionsAide.Core.HealthChecks.ExtentionMethods
{
    public static class IEndpointRouteBuilderExtensions
    {
        public static void MapHealthChecksEndPoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}
