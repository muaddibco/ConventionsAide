using ConventionsAide.Core.EntityFrameworkCore;
using ConventionsAide.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Core.Migrator
{
    public class DbMigratorService<TDbContext> : Service where TDbContext: DbContext
    {
        public DbMigratorService(ILogger<Service> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = ServiceProvider.CreateScope();
            IDbContextProvider dbContextProvider = scope.ServiceProvider.GetRequiredService<IDbContextProvider>();
            var dbContext = await dbContextProvider.GetDbContextAsync<TDbContext>();
            if (dbContext == null) return;

            await dbContext.Database.MigrateAsync(stoppingToken);
        }
    }
}
