using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ConventionsAide.Core.EntityFrameworkCore
{
    [ScopedService]
    public class DbContextProvider : IDbContextProvider 
    {
        private readonly ILazyServiceProvider _serviceProvider;

        public DbContextProvider(ILazyServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TDbContext> GetDbContextAsync<TDbContext>() where TDbContext : DbContext
        {
            return Task.FromResult(_serviceProvider.LazyGetRequiredService<TDbContext>());
        }
    }
}
