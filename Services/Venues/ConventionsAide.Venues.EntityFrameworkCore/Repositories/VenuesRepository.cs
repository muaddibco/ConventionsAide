using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.DependencyInjection;
using ConventionsAide.Core.EntityFrameworkCore;
using ConventionsAide.Core.EntityFrameworkCore.Repositories;
using ConventionsAide.Venues.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ConventionsAide.Venues.EntityFrameworkCore.Repositories
{
    [ScopedService<IVenuesRepository>]
    public class VenuesRepository : EfCoreRepository<VenuesDbContext, Venue, long>, IVenuesRepository
    {
        public VenuesRepository(IDbContextProvider dbContextProvider, ILazyServiceProvider lazyServiceProvider) : base(dbContextProvider, lazyServiceProvider)
        {
        }

        public async Task<(List<Venue>, int)> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter = null)
        {
            var count = (await GetQueryableAsync()).WhereIf(!filter.IsNullOrWhiteSpace(), v => v.Country.Contains(filter)).Count();
            var venues = await GetList(skipCount, maxResultCount, sorting, filter);
            return (venues, count);

            async Task<List<Venue>> GetList(int skipCount, int maxResultCount, string sorting, string? filter) =>
                await (await GetDbSetAsync())
                                .WhereIf(
                                    !filter.IsNullOrWhiteSpace(),
                                    v => v.Country.Contains(filter)
                                )
                                .OrderBy(NormalizeSorting(sorting))
                                .Skip(skipCount)
                                .Take(maxResultCount)
                                .ToListAsync();
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"{nameof(Venue.Name)}";
            }

            if (sorting.Contains("name"))
            {
                return sorting.Replace(
                    "name",
                    $"{nameof(Venue.Name)}"
                );
            }

            return sorting;
        }
    }
}
