using MassTransit;
using System.Threading.Tasks;
using ConventionsAide.Core.Authentication;

namespace ConventionsAide.Core.Communication
{
    public class GenericBusConsumer<TRequest, TResponse> :
        IBusConsumer<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly IBusConsumersProvider _busConsumersProvider;
        private readonly IAuthenticationProducer _authenticationProducer;

        public GenericBusConsumer(
            IBusConsumersProvider busConsumersProvider,
            IAuthenticationProducer authenticationProducer)
        {
            _busConsumersProvider = busConsumersProvider;
            _authenticationProducer = authenticationProducer;
        }

        public async Task Consume(ConsumeContext<CommandMessage<TRequest>> context)
        {
            byte[] arr = context.Headers.Get<byte[]>("authorization");

            var consumerPrincipal = await _authenticationProducer.Deserialize(arr);

            var response = await _busConsumersProvider
                .InvokeHandler<TRequest,TResponse>(context.Message, consumerPrincipal);

            await context.RespondAsync(response);
        }
    }

    public class GenericBusConsumer<TCommand> : IConsumer<TCommand> where TCommand : class
    {
        private readonly IBusConsumersProvider _busConsumersProvider;
        private readonly IAuthenticationProducer _authenticationProducer;

        public GenericBusConsumer(
            IBusConsumersProvider busConsumersProvider,
            IAuthenticationProducer authenticationProducer)
        {
            _busConsumersProvider = busConsumersProvider;
            _authenticationProducer = authenticationProducer;
        }

        public async Task Consume(ConsumeContext<TCommand> context)
        {
            byte[] arr = context.Headers.Get<byte[]>("authorization");

            var consumerPrincipal = await _authenticationProducer.Deserialize(arr);

            await _busConsumersProvider
                .InvokeCommandHandler(context.Message, consumerPrincipal);
        }
    }

}
