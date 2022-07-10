using ConventionsAide.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ConventionsAide.Core.Domain.Repositories
{
    public interface IReadOnlyRepository<TEntity> : IReadOnlyBasicRepository<TEntity>, IRepository where TEntity : class, IEntity
    {
        Task<IQueryable<TEntity>> WithDetailsAsync();

        Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<IQueryable<TEntity>> GetQueryableAsync();

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false, CancellationToken cancellationToken = default);
    }

    public interface IReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity>, IReadOnlyBasicRepository<TEntity>, IRepository, IReadOnlyBasicRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
    }
}
