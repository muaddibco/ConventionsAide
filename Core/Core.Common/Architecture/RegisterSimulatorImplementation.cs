﻿using System;

namespace ConventionsAide.Core.Common.Architecture
{
    /// <summary>
    /// Registers decorated class as simulator implementation of service specified by input argument "Implements" of the attribute constructor.
    /// </summary>
    /// <seealso cref="RegisterType" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public class RegisterSimulatorImplementation : RegisterType
    {
        public RegisterSimulatorImplementation(Type implements)
        {
            Implements = implements;
            Lifetime = LifetimeManagement.Transient;
            Role = RegistrationRole.SimulatorImplementation;
            AllowsOverride = false;
        }
    }
}
