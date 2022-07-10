using System;

namespace ConventionsAide.Core.Communication
{
    public abstract class CommandMessageBase : ICommandMessage
    {
        public CommandMessageBase(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; }
    }

    public class CommandMessage<T> : CommandMessageBase where T : class
    {
        public CommandMessage(Guid correlationId, T payload) : base(correlationId)
        {
            Payload = payload;
        }

        public T Payload { get; set; }

        public CommandMessage<TDerived> Derive<TDerived>(TDerived payload) where TDerived: class
        {
            return new CommandMessage<TDerived>(CorrelationId, payload);
        }
    }
}
