using ConventionsAide.Core.Communication.Config;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;

namespace ConventionsAide.Core.Communication
{
    public class DefaultBatchConsumerDefinition<T>: ConsumerDefinition<T> where T : class, IConsumer
    {
        private readonly int _messageLimit;
        private readonly int _timeLimitMs;
        private readonly int _concurrencyLimit;

        public DefaultBatchConsumerDefinition(IConfiguration configuration)
        {
            var communicationOptions = configuration.GetSection(CommunicationOptions.Name).Get<CommunicationOptions>();
            _messageLimit = communicationOptions.MessageLimit > 0 ? communicationOptions.MessageLimit : 20;
            _timeLimitMs = communicationOptions.TimeLimitMs > 0 ? communicationOptions.TimeLimitMs : 10000;
            _concurrencyLimit = communicationOptions.ConcurrencyLimit > 0 ? communicationOptions.ConcurrencyLimit : 1;

            Endpoint(c =>
            {
                c.PrefetchCount = 10;
            });
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<T> consumerConfigurator)
        {
            consumerConfigurator.Options<BatchOptions>(options => options
                .SetMessageLimit(_messageLimit)
                .SetTimeLimit(_timeLimitMs)
                .SetConcurrencyLimit(_concurrencyLimit));
        }
    }
}
