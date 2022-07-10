using ConventionsAide.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    public interface IApiBatchHandler<TRequest> : IApiHandler
        where TRequest : class
    {
        Task HandleAsync(IEnumerable<CommandMessage<TRequest>> request);
    }
}
