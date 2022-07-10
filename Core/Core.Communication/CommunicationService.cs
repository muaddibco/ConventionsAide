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
    [RegisterService(typeof(ICommunicationService), Lifetime = LifetimeManagement.Scoped)]
    public class CommunicationService : ICommunicationService
    {
        private const string AuthorizationHeaderName = "authorization";

        private readonly IClientFactory _clientFactory;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMessageScheduler _messageScheduler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationContext _authenticationContext;

        public CommunicationService(
            IClientFactory clientFactory,
            IPublishEndpoint publishEndpoint,
            IMessageScheduler messageScheduler,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationContext authenticationContext)
        {
            _clientFactory = clientFactory;
            _publishEndpoint = publishEndpoint;
            _messageScheduler = messageScheduler;
            _httpContextAccessor = httpContextAccessor;
            _authenticationContext = authenticationContext;
        }

        public async Task Publish<T>(Func<T> creationFunc)
            where T : class
        {
            if (creationFunc == null)
            {
                throw new ArgumentNullException(nameof(creationFunc), "can't be null!!!");
            }

            Action<PublishContext<CommandMessage<T>>> callback = (c) =>
            {
                if (_authenticationContext?.User != null)
                {
                    c.Headers.Set(AuthorizationHeaderName, SerializeUser(_authenticationContext.User));
                }
                else if (_httpContextAccessor.HttpContext != null)
                {
                    c.Headers.Set(AuthorizationHeaderName, SerializeUser(_httpContextAccessor.HttpContext.User));
                }
            };

            await _publishEndpoint.Publish(new CommandMessage<T>(Guid.NewGuid(), creationFunc()), callback);
        }

        public async Task Publish<T>(T command, int? delayInSeconds = null)
            where T : class
        {
            if (delayInSeconds.HasValue)
            {
                await _messageScheduler.SchedulePublish(DateTime.UtcNow.AddSeconds(delayInSeconds.Value), command);
                return;
            }

            await _publishEndpoint.Publish(command);
        }

        public async Task<TResponse> SendRequest<TRequest, TResponse>(TRequest payload, TimeSpan requestTimeout = default)
            where TRequest : class
            where TResponse : class
        {
            var request = _clientFactory
                .CreateRequestClient<CommandMessage<TRequest>>(requestTimeout != default && requestTimeout.TotalSeconds > 0 ? RequestTimeout.After(s: (int)requestTimeout.TotalSeconds) : default)
                .Create(new CommandMessage<TRequest>(Guid.NewGuid(), payload));

            if (_authenticationContext?.User != null)
            {
                request.UseExecute(x =>
                    x.Headers.Set(AuthorizationHeaderName, SerializeUser(_authenticationContext.User)));
            }
            else if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                request.UseExecute(x =>
                    x.Headers.Set(AuthorizationHeaderName, SerializeUser(_httpContextAccessor.HttpContext.User)));
            }

            var response = await request.GetResponse<CommandResponse<TRequest, TResponse>>();
            return response.Message.Payload;
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

        private byte[] SerializeUser(ClaimsPrincipal user)
        {
            using MemoryStream ms = new ();
            using BinaryWriter binaryWriter = new (ms);
            user.WriteTo(binaryWriter);
            return ms.ToArray();
        }
    }
}
