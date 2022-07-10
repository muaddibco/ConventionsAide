using ConventionsAide.Core.Common;
using System.Threading;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    public abstract class ApiHandlerBase<TRequest, TResponse> : ApiHandlerBase, IApiHandler<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public abstract Task<TResponse> HandleAsync(CommandMessage<TRequest> message, CancellationToken cancellationToken);
    }
}
