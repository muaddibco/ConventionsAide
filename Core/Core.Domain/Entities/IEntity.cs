namespace ConventionsAide.Core.Domain.Entities
{
    public interface IEntity
    {
        object[] GetKeys();
    }

    public interface IEntity<TKey> : IEntity
    {
        //
        // Summary:
        //     Unique identifier for this entity.
        TKey Id { get; }
    }
}
