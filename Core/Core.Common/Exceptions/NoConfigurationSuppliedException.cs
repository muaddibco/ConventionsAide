using ConventionsAide.Core.Common.Properties;
using System;

namespace ConventionsAide.Core.Common.Exceptions
{

    [Serializable]
    public class NoConfigurationSuppliedException : Exception
    {
        public NoConfigurationSuppliedException() { }
        public NoConfigurationSuppliedException(string confName) : base(string.Format(Resources.ERR_NO_CONFIG_SUPPLIED, confName)) { }
        public NoConfigurationSuppliedException(string confName, Exception inner) : base(string.Format(Resources.ERR_NO_CONFIG_SUPPLIED, confName), inner) { }
        protected NoConfigurationSuppliedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
