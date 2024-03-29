﻿using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.Architecture.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Venues.EntityFrameworkCore;

public class StartupRegistrator : StartupRegistratorBase
{
    public override void ConfigureServices(IRegistrationManager registrationManager, IServiceCollection services, IConfiguration configuration, ILogger log)
    {
        services.AddDbContext<VenuesDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Venue"), builder => builder.UseNetTopologySuite());
        });

        base.ConfigureServices(registrationManager, services, configuration, log);
    }
}
