using System.ComponentModel.Composition;
using ConventionsAide.Core.Common.Architecture.Registration;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Core.Common.Architecture
{
    [InheritedExport]
    public abstract class StartupRegistratorBase
    {
        public virtual void ConfigureServices(IRegistrationManager registrationManager, IServiceCollection services, IConfiguration configuration, ILogger log)
        {

        }

        public virtual void UseEndpoints(IEndpointRouteBuilder builder, ILogger? logger)
        {

        }
    }
}
