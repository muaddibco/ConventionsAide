using ConventionsAide.Core.Common.Architecture;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Common
{
    [RegisterExtension(typeof(IInitializer), Lifetime = LifetimeManagement.Scoped)]
    public class ApiManagersInitializer : InitializerBase
    {
        private readonly IEnumerable<IApiHandler> _apiManagers;

        public ApiManagersInitializer(IEnumerable<IApiHandler> apiManagers)
        {
            _apiManagers = apiManagers;
        }

        public override ExtensionOrderPriorities Priority => ExtensionOrderPriorities.Normal1;

        protected override async Task InitializeInner(CancellationToken cancellationToken)
        {
            foreach (var apiManager in _apiManagers)
            {
                await apiManager.Initialize().ConfigureAwait(false);
            }
        }
    }
}
