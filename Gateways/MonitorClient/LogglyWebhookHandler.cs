using HealthChecks.UI.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;

namespace ConventionsAide.HealthCheck.MonitorClient
{
    public class LogglyWebhookHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public LogglyWebhookHandler(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _environment = env;
        }

        public string RestorePayload
        {
            get
            {
                return "{\"serviceName\":\"[[LIVENESS]]\",\"healthy\":true, \"message\": \"[[LIVENESS]] service recovered\"}";
            }
        }

        public string Payload
        {
            get
            {
                return "{\"serviceName\":\"[[LIVENESS]]\",\"healthy\":false, \"entries\":\"[[FAILURE]]\", \"message\": \"[[DESCRIPTIONS]]\"}";
            }
        }

        public string BuildUrl()
        {
            var url = $"{_configuration["LogglyBaseUrl"]}/tag/HealthCheck,Incident,{_environment.EnvironmentName}";
            return url;
        }

        public string CustomMessage(UIHealthReport report)
        {
            var entires = report.Entries.Where(e => e.Value.Status != UIHealthStatus.Healthy).ToDictionary(k => k.Key, v => v.Value);
            var sb = new StringBuilder();

            foreach (var entry in entires)
            {
                sb.Append($"{entry.Key}: {entry.Value.Status} | Error: {entry.Value.Exception ?? entry.Value.Description} | Duration: {entry.Value.Duration}" + Environment.NewLine);
            }
            return sb.ToString();
        }

        public string CustomDescription(UIHealthReport report)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var entry in report.Entries.Where(e => e.Value.Status != UIHealthStatus.Healthy).OrderBy(e => e.Value.Status))
            {
                sb.Append($"Entry:{entry.Key} is {entry.Value.Status}" + Environment.NewLine);
            }

            var finalDescrption = sb.ToString();

            return finalDescrption;
        }
    }
}
