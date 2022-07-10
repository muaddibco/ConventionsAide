using ConventionsAide.Core.Authentication;
using MassTransit;
using System.Linq;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    public class GenericBatchConsumer<TRequest> : IBusBatchConsumer<TRequest> where TRequest : class
    {
        private readonly IBusConsumersProvider _busConsumersProvider;
        private readonly IAuthenticationContext _authenticationContext;

        public GenericBatchConsumer(IBusConsumersProvider busConsumersProvider, IAuthenticationContext authenticationContext)
        {
            _busConsumersProvider = busConsumersProvider;
            _authenticationContext = authenticationContext;
        }

        public async Task Consume(ConsumeContext<Batch<CommandMessage<TRequest>>> context)
        {
            var messages = context.Message.Select(s => s.Message);
            await _busConsumersProvider.InvokeBatchHandler(messages);
        }
    }
}
