using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.Architecture.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.VenueOrders.EntityFrameworkCore;

public class StartupRegistrator : StartupRegistratorBase
{
    public override void ConfigureServices(IRegistrationManager registrationManager, IServiceCollection services, IConfiguration configuration, ILogger log)
    {
        services.AddDbContext<VenueOrdersDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("VenueOrders"), builder => builder.UseNetTopologySuite());
        });

        base.ConfigureServices(registrationManager, services, configuration, log);
    }
}
