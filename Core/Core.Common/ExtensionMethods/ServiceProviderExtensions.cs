using System;
using System.Threading;
using System.Threading.Tasks;
using ConventionsAide.Core.Common.Architecture;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Core.Common.ExtensionMethods
{
    public static class ServiceProviderExtensions
    {
        public static async Task<T> UseBootstrapper<T>(this IServiceProvider serviceProvider, CancellationToken cancellationToken, ILogger logger = null) where T : Bootstrapper
        {
            if (serviceProvider is null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            T bootstrapper = (T)serviceProvider.GetService(typeof(T));
            await bootstrapper.RunInitializers(serviceProvider, cancellationToken, logger).ConfigureAwait(false);

            return bootstrapper;
        }
    }
}
