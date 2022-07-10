using ConventionsAide.Core.Common;
using ConventionsAide.Core.Common.Architecture.Registration;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using System;
using System.Linq;

namespace ConventionsAide.Core.Communication.ExtensionsMethods
{
    public static class IServiceCollectionBusConfiguratorExtentions
    {
        public static void AutoRegisterConsumers(this IServiceCollectionBusConfigurator config, IRegistrationManager registrationManager)
        {
            var typesHandlers = registrationManager.GetTypesImplementing<ApiHandlerBase>()
                .Concat(registrationManager.GetTypesImplementing<ICommandHandler>()).ToArray();
            if (typesHandlers?.Length > 0)
            {
                foreach (var handler in typesHandlers)
                {
                    var allImplementaions = handler.GetInterfaces().Where(i => i.IsGenericType);

                    foreach (var handleAction in allImplementaions)
                    {
                        var genericArgs = handleAction.GetGenericArguments();

                        if (handleAction.GetGenericTypeDefinition() == typeof(IApiHandler<,>))
                        {
                            Type genericConsumer = typeof(GenericBusConsumer<,>);
                            Type constructedClass = genericConsumer.MakeGenericType(genericArgs);
                            config.AddConsumer(constructedClass);
                        }
                        else if (handleAction.GetGenericTypeDefinition() == typeof(IApiBatchHandler<>))
                        {
                            Type genericConsumer = typeof(GenericBatchConsumer<>);
                            Type constructedClass = genericConsumer.MakeGenericType(genericArgs);
                            var definitionTypeDef = typeof(DefaultBatchConsumerDefinition<>).MakeGenericType(constructedClass);
                            config.AddConsumer(constructedClass, definitionTypeDef);
                        }
                        else if (handleAction.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                        {
                            Type genericConsumer = typeof(GenericBusConsumer<>);
                            Type constructedClass = genericConsumer.MakeGenericType(genericArgs);
                            config.AddConsumer(constructedClass);
                        }
                        else if (handleAction.GetGenericTypeDefinition() == typeof(IBroadcastHandler<>))
                        {
                            Type genericConsumer = typeof(GenericBusConsumer<>);
                            Type constructedClass = genericConsumer.MakeGenericType(genericArgs);
                            
                            Type genericDefinition = typeof(GenericBroadcastConsumerDefinition<>);
                            Type constructedDefinition = genericDefinition.MakeGenericType(genericArgs);

                            config.AddConsumer(constructedClass, constructedDefinition);
                        }
                    }
                }
            }
        }
    }
}
