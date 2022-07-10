using ConventionsAide.Core.Common.Architecture;
using System.Threading;

namespace ConventionsAide.Core.Common.Threading
{
    [ServiceContract]
    public interface ICancellationTokenProvider
    {
        CancellationToken Token { get; }

        void Cancel();

        CancellationToken FallbackToProvider(CancellationToken token = default);
    }
}
