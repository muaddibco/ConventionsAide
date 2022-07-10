using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Common;
using ConventionsAide.Core.Common.Architecture;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Communication
{
    [RegisterService(typeof(IBusConsumersProvider), Lifetime = LifetimeManagement.Scoped)]
    public class BusConsumersProvider : IBusConsumersProvider
    {
        private readonly IEnumerable<IApiHandler> _apiHandlers;
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly IAuthenticationContext _authenticatedContext;
        private readonly ILogger<BusConsumersProvider> _logger;

        public BusConsumersProvider(
            IEnumerable<IApiHandler> apiHandlers,
            IEnumerable<ICommandHandler> commandHandlers,
            IAuthenticationContext authenticatedContext,
            ILogger<BusConsumersProvider> logger)
        {
            _apiHandlers = apiHandlers;
            _commandHandlers = commandHandlers;
            _authenticatedContext = authenticatedContext;
            _logger = logger;
        }

        public async Task InvokeBatchHandler<TRequest>(IEnumerable<CommandMessage<TRequest>> request)
            where TRequest : class
        {
            if (_apiHandlers.FirstOrDefault(h => h is IApiBatchHandler<TRequest>) is not IApiBatchHandler<TRequest> handler)
            {
                throw new NotImplementedException($"Consumer for the batch of messages of type {typeof(TRequest).FullName} is not implemented");
            }

            _logger.LogDebug($"Invoking {handler.GetType().FullName} to handle batch of messages of type {typeof(TRequest).FullName}...");
            await handler.HandleAsync(request);
        }

        public async Task InvokeCommandHandler<TCommand>(TCommand command, ConsumerPrincipal consumerPrincipal)
            where TCommand : class
        {
            _authenticatedContext.User = consumerPrincipal;

            var broadcastHandler = _commandHandlers
                .Select(h => h as IBroadcastHandler<TCommand>)
                .FirstOrDefault(h => h is not null);

            if (broadcastHandler != null)
            {
                _logger.LogDebug($"Invoking {broadcastHandler.GetType().FullName} to handle broadcast command message of type {typeof(TCommand).FullName}...");
                await broadcastHandler.Handle(command);
                return;
            }

            var commandHandler = _commandHandlers
                .Select(h=> h as ICommandHandler<TCommand>)
                .FirstOrDefault(h => h is ICommandHandler<TCommand>);

            if (commandHandler != null)
            {
                _logger.LogDebug($"Invoking {commandHandler.GetType().FullName} to handle command message of type {typeof(TCommand).FullName}...");
                await commandHandler.HandleAsync(command);
                return;
            }

            throw new NotImplementedException($"Consumer for the message type {typeof(TCommand).FullName} is not implemented");
        }

        async Task<CommandResponse<TRequest, TResponse>> IBusConsumersProvider.InvokeHandler<TRequest, TResponse>(
            CommandMessage<TRequest> request,
            ConsumerPrincipal consumerPrincipal)
        {
            if (_apiHandlers.FirstOrDefault(h => h is IApiHandler<TRequest, TResponse>) is not IApiHandler<TRequest, TResponse> handler)
            {
                throw new NotImplementedException($"Consumer for the message type {typeof(TRequest).FullName} is not implemented");
            }

            _authenticatedContext.User = consumerPrincipal;

            _logger.LogDebug($"Invoking {handler.GetType().FullName} to handle request message of type {typeof(TRequest).FullName} and return response of type {typeof(TResponse).FullName}>...");
            var response = await handler.HandleAsync(request, default);

            return new CommandResponse<TRequest, TResponse>(
                request.CorrelationId,
                request.Payload,
                response);
        }
    }
}
