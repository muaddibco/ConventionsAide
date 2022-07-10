using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NLog.Layouts;
using NLog.Targets;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConventionsAide.Core.HealthChecks
{
    public class LogglyHealthCheck : IHealthCheck
    {
        private static string _testUrl;
        private const string _logglySuccessResponse = "{\"response\" : \"ok\"}";
        private static Exception _setupException = null;
        private static bool _initialized = false;

        public LogglyHealthCheck(IWebHostEnvironment env)
        {
            if (_initialized == false)
            {
                _initialized = true;
                _testUrl = initTestUrl(env);
            }
            
        }

        private string initTestUrl(IWebHostEnvironment env)
        {
            try
            {
                var environment = env.EnvironmentName;
                var factory = NLogBuilder.ConfigureNLog($"nlog.{environment}.config");
                var target = factory.Configuration.AllTargets.Where(t => t is LogglyTarget).Select(t => t as LogglyTarget).FirstOrDefault();

                if (target == null)
                {
                    throw new ArgumentNullException("No Loggly Target found");
                }

                var tags = string.Join(",", target.Tags.Select(t => (t.Name as SimpleLayout).Text)) + ",HealthCheck";
                var url = $"{target.LogTransport}://{(target.EndpointHostname as SimpleLayout).Text}:{(target.EndpointPort as SimpleLayout).Text}/inputs/{(target.CustomerToken as SimpleLayout).Text}/tag/{tags}";
                return url;
            }
            catch (Exception ex)
            {
                _setupException = ex;
                return null;
            }
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (_setupException != null)
                {
                    return HealthCheckResult.Unhealthy(_setupException.Message, _setupException);
                }

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_testUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody == _logglySuccessResponse)
                {
                    return HealthCheckResult.Healthy();
                }
                else
                {
                    var args = new Dictionary<string, object>() { {"responseBody", responseBody } };
                    return HealthCheckResult.Degraded("Http response status 200 but response body is unexpected", null, new ReadOnlyDictionary<string, object>(args));
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Degraded("Exception while checking Loggly health", ex);
            }
        }
    }
}
