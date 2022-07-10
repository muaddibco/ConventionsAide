using MassTransit;
using System;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication.ExtensionsMethods
{
    public static class EndpointExtensions
    {
        public static async Task PublishCommand<T>(this IPublishEndpoint publishEndpoint, Func<T> creationFunc) where T:class
        {
            if (creationFunc == null)
            {
                throw new ArgumentNullException(nameof(creationFunc), "can't be null!!!");
            }

            await publishEndpoint.Publish(new CommandMessage<T>(Guid.NewGuid(), creationFunc()));
        }

        public static async Task<Response<CommandResponse<TRequest, TResponse>>> SendRequest<TRequest, TResponse>(this IRequestClient<CommandMessage<TRequest>> requestClient, TRequest payload) where TRequest : class where TResponse : class
        {
            return await requestClient.GetResponse<CommandResponse<TRequest, TResponse>>(new CommandMessage<TRequest>(Guid.NewGuid(), payload));
        }

        public static async Task<Response<CommandResponse<TRequest, TResponse>>> SendRequest<TRequest, TResponse>(this IRequestClient<CommandMessage<TRequest>> requestClient, TRequest payload, RequestTimeout timeout) where TRequest : class where TResponse : class
        {
            return await requestClient.GetResponse<CommandResponse<TRequest, TResponse>>(new CommandMessage<TRequest>(Guid.NewGuid(), payload), timeout: timeout);
        }

        public static async Task<Response<CommandResponse<TRequest, TResponse>>> SendRequest<TRequest, TResponse>(this IRequestClient<CommandMessage<TRequest>> requestClient, Guid guid, TRequest payload) where TRequest : class where TResponse : class
        {
            return await requestClient.GetResponse<CommandResponse<TRequest, TResponse>>(new CommandMessage<TRequest>(guid, payload));
        }
    }
}
