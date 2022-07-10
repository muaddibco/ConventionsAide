using System;


namespace ConventionsAide.Core.Common.Architecture
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class RegisterWithSimulatorFactory : RegisterType
    {
        public RegisterWithSimulatorFactory(Type factoryType)
        {
            Factory = factoryType;
            Lifetime = LifetimeManagement.Transient;
            Role = RegistrationRole.SimulatorImplementation;
            AllowsOverride = true;
        }
    }
}
