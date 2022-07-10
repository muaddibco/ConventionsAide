using MassTransit;
using System;

namespace ConventionsAide.Core.Communication
{
    public interface ICommandMessage : CorrelatedBy<Guid>
    {
        //ICommandMessage ConstructDerived<T>(ICommandMessage source) where T : class, ICommandMessage;
    }
}
