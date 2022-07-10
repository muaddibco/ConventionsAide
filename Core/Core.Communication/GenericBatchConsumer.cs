using MassTransit;
using System.Linq;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    public class GenericBatchConsumer<TRequest> : IBusBatchConsumer<TRequest> where TRequest : class
    {
        private readonly IBusConsumersProvider _busConsumersProvider;
        public GenericBatchConsumer(IBusConsumersProvider busConsumersProvider)
        {
            _busConsumersProvider = busConsumersProvider;
        }

        public async Task Consume(ConsumeContext<Batch<CommandMessage<TRequest>>> context)
        {
            var messages = context.Message.Select(s => s.Message);
            await _busConsumersProvider.InvokeBatchHandler(messages);
        }
    }
}
