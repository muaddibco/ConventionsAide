using PostSharp.Patterns.Contracts;
using System;

namespace ConventionsAide.Core.Common
{
    public class DisposeAction : IDisposable
    {
        private readonly Action _action;

        //
        // Summary:
        //     Creates a new Volo.Abp.DisposeAction object.
        //
        // Parameters:
        //   action:
        //     Action to be executed when this object is disposed.
        public DisposeAction([NotNull] Action action) => _action = action;

        public void Dispose() => _action();
    }
}
