using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Domain.Repositories;

namespace ConventionsAide.Venues.Domain
{
    [ServiceContract]
    public interface IVenuesRepository : IRepository<Venue, long>
    {
        Task<(List<Venue>, int)> GetListAsync(int skipCount, int maxResultCount, string sorting, string? filter = null);
    }
}
