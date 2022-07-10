using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.Architecture.Registration;
using ConventionsAide.Venues.Integration.OpenBrewery.Manager.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Venues.Integration.OpenBrewery.Manager;

public class StartupRegistrator : StartupRegistratorBase
{
    public override void ConfigureServices(IRegistrationManager registrationManager, IServiceCollection services, IConfiguration configuration, ILogger log)
    {
        base.ConfigureServices(registrationManager, services, configuration, log);
        services.Configure<OpenBreweryOptions>(configuration.GetSection(OpenBreweryOptions.SectionName));
    }
}
