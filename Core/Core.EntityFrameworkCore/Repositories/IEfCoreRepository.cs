using ConventionsAide.Core.Domain.Entities;
using ConventionsAide.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConventionsAide.Core.EntityFrameworkCore.Repositories
{
    public interface IEfCoreRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
    {
        Task<DbContext> GetDbContextAsync();

        Task<DbSet<TEntity>> GetDbSetAsync();
    }

    public interface IEfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
