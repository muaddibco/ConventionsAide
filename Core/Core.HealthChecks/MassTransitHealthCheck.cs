using ConventionsAide.Core.Communication;
using ConventionsAide.Core.Communication.ExtensionsMethods;
using ConventionsAide.Core.HealthChecks.Logic.MassTransit;
using MassTransit;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace ConventionsAide.Core.HealthChecks
{
    public class MassTransitHealthCheck : IHealthCheck
    {
        private readonly IRequestClient<CommandMessage<MassTransitCheckRequest>> _requestClient;

        public MassTransitHealthCheck(IRequestClient<CommandMessage<MassTransitCheckRequest>> requestClient)
        {
            _requestClient = requestClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new MassTransitCheckRequest() { TestGuid = Guid.NewGuid() };
                var response = await _requestClient.SendRequest<MassTransitCheckRequest,MassTransitCheckResponse>(request, timeout: RequestTimeout.After(s: 5));
                if (request.TestGuid == response.Message.Payload.TestGuid)
                {
                    return HealthCheckResult.Healthy();
                }
                var args = new Dictionary<string, object>() { { "request", request }, { "response", response.Message }, { "CorrelationId", response.CorrelationId } };
                return HealthCheckResult.Unhealthy("Recieved unexpected response", data: new ReadOnlyDictionary<string, object>(args));
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message, ex);
            }
        }
    }
}
