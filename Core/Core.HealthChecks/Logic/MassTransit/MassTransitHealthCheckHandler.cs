using ConventionsAide.Core.Communication;
using System.Threading;
using System.Threading.Tasks;

namespace ConventionsAide.Core.HealthChecks.Logic.MassTransit
{
    public class MassTransitHealthCheckHandler : ApiHandlerBase<MassTransitCheckRequest, MassTransitCheckResponse>
    {
        public override Task<MassTransitCheckResponse> HandleAsync(CommandMessage<MassTransitCheckRequest> message, CancellationToken cancellationToken)
        {
            return Task.FromResult(new MassTransitCheckResponse() { TestGuid = message.Payload.TestGuid });
        }
    }
}
