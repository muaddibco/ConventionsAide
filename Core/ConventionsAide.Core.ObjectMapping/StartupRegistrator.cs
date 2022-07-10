using AutoMapper;
using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.Architecture.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Core.ObjectMapping
{
    public class StartupRegistrator : StartupRegistratorBase
    {
        public override void ConfigureServices(IRegistrationManager registrationManager, IServiceCollection services, IConfiguration configuration, ILogger log)
        {
            var profileTypes = registrationManager.GetAllTypesImplementing<Profile>();
            services.AddAutoMapper(profileTypes);

            base.ConfigureServices(registrationManager, services, configuration, log);
        }
    }
}
