using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.ExtensionMethods;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Services
{
    public class Service : BackgroundService
    {
        private readonly ILogger<Service> _logger;

        protected IServiceProvider ServiceProvider { get; }

        public Service(ILogger<Service> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            ServiceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await ServiceProvider.UseBootstrapper<Bootstrapper>(cancellationToken, _logger);
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }
}
