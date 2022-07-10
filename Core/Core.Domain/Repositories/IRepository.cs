using ConventionsAide.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ConventionsAide.Core.Domain.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>, IReadOnlyBasicRepository<TEntity>, IRepository, IBasicRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));
    }

    public interface IRepository<TEntity, TKey> : IRepository<TEntity>, IReadOnlyRepository<TEntity>, IReadOnlyBasicRepository<TEntity>, IRepository, IBasicRepository<TEntity>, IReadOnlyRepository<TEntity, TKey>, IReadOnlyBasicRepository<TEntity, TKey>, IBasicRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
    }
}
