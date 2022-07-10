using ConventionsAide.Core.Common.Aspects;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Reflection;

namespace ConventionsAide.Core.Communication
{
    [PSerializable]
    public class CommunicationAutoLogAttribute : AutoLogAttribute
    {
        public override bool CompileTimeValidate(MethodBase method)
        {
            return typeof(IBusController).IsAssignableFrom(method.DeclaringType);
        }

        protected override string GetOnEntryMessage(MethodExecutionArgs args)
        {
            if (args.Method.Name == "ConsumeInner")
            {
                return $"{base.GetOnEntryMessage(args)}; {JsonConvert.SerializeObject(args.Arguments.GetArgument(0), Formatting.Indented)}";
            }
            
            return $"{base.GetOnEntryMessage(args)}";
        }
    }
}
