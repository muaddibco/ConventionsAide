using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Linq;
using ConventionsAide.Core.Common.Architecture;

namespace ConventionsAide.Core.Communication
{
    [CommunicationAutoLog]
    [RegisterExtension(typeof(IBusController), Lifetime = LifetimeManagement.Scoped)]
    public abstract class ConsumerBase : IBusController
    {

        public ConsumerBase(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger(GetType().Name);
            ServiceProvider = serviceProvider;
        }

        protected ILogger Logger {get;}
        protected IServiceProvider ServiceProvider { get; }

        protected async Task ConsumeInner<TRequest, TResponse>(ConsumeContext<CommandMessage<TRequest>> context) where TRequest : class where TResponse : class
        {
            var response = await ServiceProvider.GetService<IBusConsumersProvider>().InvokeHandler<TRequest, TResponse>(context.Message).ConfigureAwait(false);
            await context.RespondAsync(response).ConfigureAwait(false);
        }

        protected async Task ConsumeInner<TRequest>(ConsumeContext<Batch<CommandMessage<TRequest>>> batch) where TRequest : class
        {
            await ServiceProvider.GetService<IBusConsumersProvider>().InvokeBatchHandler(batch.Message.Select(s => s.Message)).ConfigureAwait(false);
        }
    }
}
