namespace ConventionsAide.Core.Contracts
{
    public abstract class EntityDto<TKey>
    {
        public TKey Id { get; set; }
    }
}
