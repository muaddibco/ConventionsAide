using ConventionsAide.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConventionsAide.Core.EntityFrameworkCore.Repositories
{
    public interface IEfCoreBulkOperationProvider
    {
        Task InsertManyAsync<TDbContext, TEntity>(
            IEfCoreRepository<TEntity> repository,
            IEnumerable<TEntity> entities,
            bool autoSave,
            CancellationToken cancellationToken
        )
            where TDbContext : DbContext
            where TEntity : class, IEntity;


        Task UpdateManyAsync<TDbContext, TEntity>(
            IEfCoreRepository<TEntity> repository,
            IEnumerable<TEntity> entities,
            bool autoSave,
            CancellationToken cancellationToken
        )
            where TDbContext : DbContext
            where TEntity : class, IEntity;


        Task DeleteManyAsync<TDbContext, TEntity>(
            IEfCoreRepository<TEntity> repository,
            IEnumerable<TEntity> entities,
            bool autoSave,
            CancellationToken cancellationToken
        )
            where TDbContext : DbContext
            where TEntity : class, IEntity;
    }
}
