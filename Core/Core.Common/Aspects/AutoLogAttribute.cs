using ConventionsAide.Core.Logging.Extensions;
using Microsoft.Extensions.Logging;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Serialization;
using System.Reflection;

namespace ConventionsAide.Core.Common.Aspects
{
    [MulticastAttributeUsage(MulticastTargets.Method, Inheritance = MulticastInheritance.Strict)]
    [PSerializable]
    public class AutoLogAttribute : OnMethodBoundaryAspect
    {
        private ILogger _logger;
        private ILoggerFactory _loggerFactory;

        public AutoLogAttribute()
        {
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            base.RuntimeInitialize(method);


            _loggerFactory = AspectServiceLocator.GetService<ILoggerFactory>();
            _logger = _loggerFactory.CreateLogger(method.DeclaringType.Name);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            base.OnEntry(args);

            _logger.Debug(() => GetOnEntryMessage(args));
        }

        protected virtual string GetOnEntryMessage(MethodExecutionArgs args)
        {
            return $"Started method {args.Method.Name} of the class {args.Instance?.GetType().FullName}";
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            base.OnExit(args);

            _logger.Debug(() => GetOnExitMessage(args));
        }

        protected virtual string GetOnExitMessage(MethodExecutionArgs args)
        {
            return $"Exited method {args.Method.Name} of the class {args.Instance?.GetType().FullName}";
        }
    }
}
