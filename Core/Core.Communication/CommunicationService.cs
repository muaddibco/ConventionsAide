using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Common.Architecture;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace ConventionsAide.Core.Communication
{
    [ScopedService]
    public class CommunicationService : ICommunicationService
    {
        public const string AuthorizationHeaderName = "authorization";
        public const string AuthorizationApiHeaderName = "authorizationApi";

        private readonly IClientFactory _clientFactory;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMessageScheduler _messageScheduler;
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly IAuthenticationContext _authenticationContext;
        private readonly IAuthenticationProducer _authenticationProducer;

        public CommunicationService(
            IClientFactory clientFactory,
            IPublishEndpoint publishEndpoint,
            IMessageScheduler messageScheduler,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationContext authenticationContext,
            IAuthenticationProducer authenticationProducer)
        {
            _clientFactory = clientFactory;
            _publishEndpoint = publishEndpoint;
            _messageScheduler = messageScheduler;
            _httpContextAccessor = httpContextAccessor;
            _authenticationContext = authenticationContext;
            _authenticationProducer = authenticationProducer;
        }

        public async Task Publish<T>(Func<T> creationFunc, string? apiName = null)
            where T : class
        {
            if (creationFunc == null)
            {
                throw new ArgumentNullException(nameof(creationFunc), "can't be null!!!");
            }

            await _publishEndpoint.Publish(
                    new CommandMessage<T>(Guid.NewGuid(), creationFunc()), 
                    async c => 
                    {
                        SetAuthorizationHeader(c.Headers);
                        await SetAuthorizationApiHeader(apiName, c.Headers);
                    });
        }

        public async Task Publish<T>(T command, int? delayInSeconds = null, string? apiName = null)
            where T : class
        {
            if (delayInSeconds.HasValue)
            {
                await _messageScheduler.SchedulePublish(DateTime.UtcNow.AddSeconds(delayInSeconds.Value), command);
                return;
            }

            await _publishEndpoint.Publish(command, async c =>
            {
                if (!apiName.IsNullOrEmpty())
                {
                    await SetAuthorizationApiHeader(apiName, c.Headers);
                }
            });
        }

        public async Task<TResponse> SendRequest<TRequest, TResponse>(TRequest payload, string? apiName = null, TimeSpan requestTimeout = default)
            where TRequest : class
            where TResponse : class
        {
            var request = _clientFactory
                .CreateRequestClient<CommandMessage<TRequest>>(requestTimeout != default && requestTimeout.TotalSeconds > 0 ? RequestTimeout.After(s: (int)requestTimeout.TotalSeconds) : default)
                .Create(new CommandMessage<TRequest>(Guid.NewGuid(), payload));

            request.UseExecute(async x =>
            {
                SetAuthorizationHeader(x.Headers);

                await SetAuthorizationApiHeader(apiName, x.Headers);
            });

            var response = await request.GetResponse<CommandResponse<TRequest, TResponse>>();
            return response.Message.Payload;
        }

        private void SetAuthorizationHeader(SendHeaders headers)
        {
            if (_authenticationContext?.User != null)
            {
                headers.Set(AuthorizationHeaderName, _authenticationProducer.Serialize(_authenticationContext.User));
            }
            else if (_httpContextAccessor?.HttpContext?.User.Identity?.IsAuthenticated ?? false)
            {
                headers.Set(AuthorizationHeaderName, _authenticationProducer.Serialize(_httpContextAccessor.HttpContext.User));
            }
        }

        private async Task SetAuthorizationApiHeader(string? apiName, SendHeaders headers)
        {
            if (!apiName.IsNullOrEmpty())
            {
                var apiToken = await _authenticationContext?.FetchApiToken(apiName);
                headers.Set(AuthorizationApiHeaderName, apiToken);
            }
        }

        //public async Task<IOptions<TResponse>> SendRequestWithOptions<TRequest, TResponse>(
        //    TRequest payload,
        //    Func<TResponse, IOptions<TResponse>> creation)
        //    where TRequest : class
        //    where TResponse : class
        //{
        //    var response = await SendRequest<TRequest, TResponse>(payload);
        //    return creation(response);
        //}
    }
}
