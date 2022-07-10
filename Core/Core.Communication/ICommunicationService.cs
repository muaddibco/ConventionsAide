using System;
using System.Threading.Tasks;
using ConventionsAide.Core.Common.Architecture;
using MassTransit;

namespace ConventionsAide.Core.Communication
{
    [ServiceContract]
    public interface ICommunicationService
    {
        Task Publish<T>(Func<T> creationFunc, string? apiName = null) where T : class;

        Task Publish<T>(T command, int? delayInSeconds = null, string? apiName = null) where T : class;

        Task<TResponse> SendRequest<TRequest, TResponse>(TRequest payload, string? apiName = null, TimeSpan requestTimeout = default)
            where TRequest : class
            where TResponse : class;

        //public Task<IOptions<TResponse>> SendRequestWithOptions<TRequest, TResponse>(TRequest payload, Func<TResponse, IOptions<TResponse>> creation)
        //    where TRequest : class
        //    where TResponse : class;
    }
}
