using ConventionsAide.Core.Common.Architecture;
using Microsoft.EntityFrameworkCore;

namespace ConventionsAide.Core.EntityFrameworkCore
{
    [ServiceContract]
    public interface IDbContextProvider 
    {
        Task<TDbContext> GetDbContextAsync<TDbContext>() where TDbContext : DbContext;
    }
}
