using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Common.Architecture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    [ServiceContract]
    public interface IBusConsumersProvider
    {
        Task<CommandResponse<TRequest, TResponse>> InvokeHandler<TRequest, TResponse>(CommandMessage<TRequest> request) where TRequest : class where TResponse : class;

        Task InvokeCommandHandler<TCommand>(TCommand command) where TCommand : class;

        Task InvokeBatchHandler<TRequest>(IEnumerable<CommandMessage<TRequest>> request) where TRequest : class;
    }
}
