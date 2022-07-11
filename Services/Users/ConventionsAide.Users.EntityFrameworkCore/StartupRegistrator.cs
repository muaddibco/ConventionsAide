using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.Architecture.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Users.EntityFrameworkCore;

public class StartupRegistrator : StartupRegistratorBase
{
    public override void ConfigureServices(IRegistrationManager registrationManager, IServiceCollection services, IConfiguration configuration, ILogger log)
    {
        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Users"), builder => builder.UseNetTopologySuite());
        });

        base.ConfigureServices(registrationManager, services, configuration, log);
    }
}
