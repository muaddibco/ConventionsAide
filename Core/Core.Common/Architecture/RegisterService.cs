using System;

namespace ConventionsAide.Core.Common.Architecture
{
    /// <summary>
    /// Registers decorated class as default implementation of service specified by input argument "Implements" of the attribute constructor.
    /// </summary>
    /// <seealso cref="RegisterType" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class RegisterService : RegisterType
    {
        public RegisterService(LifetimeManagement lifetimeManagement = LifetimeManagement.Transient)
        {
            Lifetime = lifetimeManagement;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = false;
        }

        public RegisterService(Type implements)
            : this()
        {
            Implements = implements;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class RegisterService<T> : RegisterType
    {
        public RegisterService(LifetimeManagement lifetime = LifetimeManagement.Transient)
        {
            Implements = typeof(T);
            Lifetime = lifetime;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = false;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class ScopedServiceAttribute: RegisterService
    {
        public ScopedServiceAttribute()
        {
            Lifetime = LifetimeManagement.Scoped;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = false;
        }

        public ScopedServiceAttribute(Type implements)
            : this()
        {
            Implements = implements;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class TransientServiceAttribute : RegisterService
    {
        public TransientServiceAttribute()
        {
            Lifetime = LifetimeManagement.Transient;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = false;
        }

        public TransientServiceAttribute(Type implements)
            : this()
        {
            Implements = implements;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class SingletonServiceAttribute : RegisterService
    {
        public SingletonServiceAttribute()
        {
            Lifetime = LifetimeManagement.Singleton;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = false;
        }

        public SingletonServiceAttribute(Type implements)
            : this()
        {
            Implements = implements;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class ScopedServiceAttribute<T> : RegisterService
    {
        public ScopedServiceAttribute()
        {
            Lifetime = LifetimeManagement.Scoped;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = false;
            Implements = typeof(T);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class TransientServiceAttribute<T> : RegisterService
    {
        public TransientServiceAttribute()
        {
            Lifetime = LifetimeManagement.Transient;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = false;
            Implements = typeof(T);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class SingletonServiceAttribute<T> : RegisterService
    {
        public SingletonServiceAttribute()
        {
            Lifetime = LifetimeManagement.Singleton;
            Role = RegistrationRole.DefaultImplementation;
            AllowsOverride = false;
            Implements = typeof(T);
        }
    }
}
