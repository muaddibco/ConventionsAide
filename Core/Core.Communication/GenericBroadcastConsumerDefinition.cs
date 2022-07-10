using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using System;

namespace ConventionsAide.Core.Communication
{
    public class GenericBroadcastConsumerDefinition<TCommand> : ConsumerDefinition<GenericBusConsumer<TCommand>> where TCommand : class
    {
        public GenericBroadcastConsumerDefinition()
        {
            EndpointName = $"{typeof(TCommand)}_Broadcast_{Guid.NewGuid()}";
            Endpoint(c =>  c.Temporary = true );
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GenericBusConsumer<TCommand>> consumerConfigurator)
        {
            base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
        }
    }
}
