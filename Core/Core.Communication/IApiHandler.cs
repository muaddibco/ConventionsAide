using System.Threading;
using System.Threading.Tasks;
using ConventionsAide.Core.Common;

namespace ConventionsAide.Core.Communication
{
    /// <summary>
    /// Marks the entry point in the microservice.
    /// </summary>
    public interface IApiHandler<TRequest, TResponse> : IApiHandler
        where TRequest : class
        where TResponse : class
    {
        /// <summary>
        /// Handles the request.
        /// </summary>
        Task<TResponse> HandleAsync(CommandMessage<TRequest> message, CancellationToken cancellationToken);
    }
}
