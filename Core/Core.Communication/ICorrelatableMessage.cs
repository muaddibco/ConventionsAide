namespace ConventionsAide.Core.Communication
{
    public interface ICorrelatableMessage
    {
        string CorrelationId { get; set; }
    }
}
