namespace ConventionsAide.Core.Domain.Exceptions
{

    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }
        public EntityNotFoundException(Type entityType, object? key = null) : base($"Entity of type {entityType.Name} with id {key?.ToString()} not found") { }
        public EntityNotFoundException(Type entityType, object key, Exception inner) : base($"Entity of type {entityType.Name} with id {key?.ToString()} not found", inner) { }
        protected EntityNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
