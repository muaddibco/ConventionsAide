using MassTransit;

namespace ConventionsAide.Core.Communication
{
    public interface IBusConsumer<TRequest, TResponse> : IBusController, IConsumer<CommandMessage<TRequest>> where TRequest : class where TResponse : class
    {
    }
}
