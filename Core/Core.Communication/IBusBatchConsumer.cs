using MassTransit;

namespace ConventionsAide.Core.Communication
{
    public interface IBusBatchConsumer<TRequest> : IBusBatchController, IConsumer<Batch<CommandMessage<TRequest>>> where TRequest : class
    {
    }
}
