using System.Threading;
using System.Threading.Tasks;
using ConventionsAide.Core.Common.Architecture;


namespace ConventionsAide.Core.Common
{
    [ExtensionPoint]
    public interface IInitializer
    {
        ExtensionOrderPriorities Priority { get; }

        bool Initialized { get; }

        Task Initialize(CancellationToken cancellationToken);
    }
}
