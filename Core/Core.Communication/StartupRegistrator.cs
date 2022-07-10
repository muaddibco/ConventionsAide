using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.Architecture.Registration;
using ConventionsAide.Core.Common.Exceptions;
using ConventionsAide.Core.Communication.Config;
using ConventionsAide.Core.Communication.ExtensionsMethods;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Core.Communication
{
    public class StartupRegistrator : StartupRegistratorBase
    {
        public override void ConfigureServices(IRegistrationManager registrationManager, Microsoft.Extensions.DependencyInjection.IServiceCollection services, IConfiguration configuration, ILogger log)
        {
            base.ConfigureServices(registrationManager, services, configuration, log);

            services.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((ctx, conf) =>
                    {
                        var communicationOptions = configuration.GetSection(CommunicationOptions.Name).Get<CommunicationOptions>();
                        if (communicationOptions == null)
                        {
                            throw new NoConfigurationSuppliedException(CommunicationOptions.Name);
                        }
                        conf.Host(communicationOptions.Host, communicationOptions.VirtualHost, h =>
                        {
                            h.Username(communicationOptions.Username);
                            h.Password(communicationOptions.Password);
                        });

                        conf.ConnectReceiveObserver(new ReceiveObserver());
                        conf.UseDelayedMessageScheduler();
                        conf.ConfigureEndpoints(ctx);
                        conf.MessageTopology.SetEntityNameFormatter(new KebabEntityNameFormatter());
                    });

                    x.AddDelayedMessageScheduler();
                    x.SetKebabCaseEndpointNameFormatter();

                    x.AutoRegisterConsumers(registrationManager);
                })
                .AddGenericRequestClient()
                .AddMassTransitHostedService();
        }
    }
}
