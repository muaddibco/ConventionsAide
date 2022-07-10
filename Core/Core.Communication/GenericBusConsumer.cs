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
        private readonly IAuthenticationContext _authenticationContext;

        public GenericBusConsumer(
            IBusConsumersProvider busConsumersProvider,
            IAuthenticationProducer authenticationProducer,
            IAuthenticationContext authenticationContext)
        {
            _busConsumersProvider = busConsumersProvider;
            _authenticationProducer = authenticationProducer;
            _authenticationContext = authenticationContext;
        }

        public async Task Consume(ConsumeContext<CommandMessage<TRequest>> context)
        {
            _authenticationContext.SetUserFromHeader(context.Headers.Get<byte[]>(CommunicationService.AuthorizationHeaderName));

            string apiToken = context.Headers.Get<string>(CommunicationService.AuthorizationApiHeaderName);

            var response = await _busConsumersProvider.InvokeHandler<TRequest,TResponse>(context.Message);

            await context.RespondAsync(response);
        }
    }

    public class GenericBusConsumer<TCommand> : IConsumer<TCommand> where TCommand : class
    {
        private readonly IBusConsumersProvider _busConsumersProvider;
        private readonly IAuthenticationProducer _authenticationProducer;
        private readonly IAuthenticationContext _authenticationContext;

        public GenericBusConsumer(
            IBusConsumersProvider busConsumersProvider,
            IAuthenticationProducer authenticationProducer,
            IAuthenticationContext authenticationContext)
        {
            _busConsumersProvider = busConsumersProvider;
            _authenticationProducer = authenticationProducer;
            _authenticationContext = authenticationContext;
        }

        public async Task Consume(ConsumeContext<TCommand> context)
        {
            _authenticationContext.SetUserFromHeader(context.Headers.Get<byte[]>("authorization"));

            await _busConsumersProvider.InvokeCommandHandler(context.Message);
        }
    }

}
