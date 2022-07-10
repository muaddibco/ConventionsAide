using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.Architecture.Registration;
using ConventionsAide.Core.Common.Exceptions;
using ConventionsAide.Core.Communication.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConventionsAide.Core.HealthChecks.ExtentionMethods;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;

namespace ConventionsAide.Core.HealthChecks
{
    public class StartupRegistrator : StartupRegistratorBase
    {
        public override void ConfigureServices(IRegistrationManager registrationManager, IServiceCollection services, IConfiguration configuration, ILogger log)
        {
            base.ConfigureServices(registrationManager, services, configuration, log);

            var co = configuration.GetSection(CommunicationOptions.Name).Get<CommunicationOptions>();
            if (co == null)
            {
                throw new NoConfigurationSuppliedException(CommunicationOptions.Name);
            }

            services.AddHealthChecks()
                .AddBuildVersionHealthCheck()
                .AddRabbitMQ($"amqp://{co.Username}:{co.Password}@{co.Host}:5672{co.VirtualHost}", sslOption: null, tags: new[] { "rabbit_mq", "infra" })
                //.AddLogglyHealthCheck()
                .AddMassTransitHealthCheck();
        }

        public override void UseEndpoints(IEndpointRouteBuilder builder, ILogger logger)
        {
            builder.MapHealthChecksEndPoint();
            base.UseEndpoints(builder, logger);
        }
    }
}
