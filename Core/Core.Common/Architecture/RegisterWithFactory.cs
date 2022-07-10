using System;


namespace ConventionsAide.Core.Common.Architecture
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class RegisterWithFactory : RegisterType
    {
        public RegisterWithFactory(Type factoryType)
        {
            Factory = factoryType;
            Lifetime = LifetimeManagement.Transient;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = true;
        }
    }
}
