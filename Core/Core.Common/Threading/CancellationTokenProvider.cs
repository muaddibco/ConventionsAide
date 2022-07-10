using ConventionsAide.Core.Common.Architecture;
using System;
using System.Threading;

namespace ConventionsAide.Core.Common.Threading
{
    [RegisterService(typeof(ICancellationTokenProvider), Lifetime = LifetimeManagement.Scoped)]
    public class CancellationTokenProvider : ICancellationTokenProvider, IDisposable
    {
        private bool _disposedValue;
        private CancellationTokenSource _cancellationTokenSource = new();

        public CancellationToken Token => _cancellationTokenSource?.Token ?? default;

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Cancel();
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public CancellationToken FallbackToProvider(CancellationToken token = default)
        {
            if (token == default)
            {
                return Token;
            }

            return token;
        }
    }
}
