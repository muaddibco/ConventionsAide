using System;
using ConventionsAide.Core.Common.Properties;

namespace ConventionsAide.Core.Common.Exceptions
{

    [Serializable]
    public class CircularClassReferenceException : Exception
    {
        public CircularClassReferenceException() { }
        public CircularClassReferenceException(Type type1, Type type2) : base(string.Format(Resources.ERR_CIRCULAR_CLASS_REFERENCE, type1.FullName, type2.FullName)) { }
        public CircularClassReferenceException(Type type1, Type type2, Exception inner) : base(string.Format(Resources.ERR_CIRCULAR_CLASS_REFERENCE, type1.FullName, type2.FullName), inner) { }
        protected CircularClassReferenceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
