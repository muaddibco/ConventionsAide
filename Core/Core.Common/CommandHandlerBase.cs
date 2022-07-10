using ConventionsAide.Core.Common.Architecture;

namespace ConventionsAide.Core.Common
{
    [RegisterExtension(typeof(ICommandHandler), Lifetime = LifetimeManagement.Scoped)]
    public abstract class CommandHandlerBase : ICommandHandler
    {
    }
}
