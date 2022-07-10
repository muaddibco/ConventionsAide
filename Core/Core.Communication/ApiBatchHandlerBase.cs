using ConventionsAide.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    public abstract class ApiBatchHandlerBase<TRequest> : ApiHandlerBase, IApiBatchHandler<TRequest> where TRequest : class
    {
        public abstract Task HandleAsync(IEnumerable<CommandMessage<TRequest>> request);
    }
}
