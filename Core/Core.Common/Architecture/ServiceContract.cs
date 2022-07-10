using System;
using ConventionsAide.Core.Common.ExtensionMethods;

namespace ConventionsAide.Core.Common.Architecture
{
    /// <summary>
    /// Attribute decorating classes or interfaces and designating definition for services
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class ServiceContract : Attribute
    {
        public Type Contract { get; set; }

        public override string ToString()
        {
            return $"Service Contract - {Contract.FullNameWithAssemblyPath()}";
        }
    }
}
