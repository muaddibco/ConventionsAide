using ConventionsAide.Core.Common.Architecture;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Common
{
    [RegisterExtension(typeof(IApiHandler), Lifetime = LifetimeManagement.Scoped)]
    public abstract class ApiHandlerBase : IApiHandler
    {
        public virtual async Task Initialize()
        {
            await Task.CompletedTask;
        }
    }
}
