using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ConventionsAide.Core.Common.Exceptions;
using ConventionsAide.Core.Common.ExtensionMethods;
using Microsoft.Extensions.Logging;
using ConventionsAide.Core.Logging.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;

namespace ConventionsAide.Core.Common.Architecture.Registration
{
    internal class RegistrationManager : IRegistrationManager
    {
        private readonly ILogger _logger;
        private IEnumerable<StartupRegistratorBase> _startupRegistrators;

        private List<ExtensionPoint> ExtensionPoints { get; }
        private List<ServiceContract> ServiceContracts { get; }
        private List<RegisterType> TypeRegistrations { get; }
        private HashSet<int> RegisteredTypeRegistrations { get; }

        public RunMode CurrentRunMode { get; set; }

        public IServiceCollection Services { get; }

        internal RegistrationManager(RunMode runMode, IServiceCollection services, ILogger logger)
        {
            Services = services;
            _logger = logger;
            ExtensionPoints = new List<ExtensionPoint>();
            ServiceContracts = new List<ServiceContract>();
            TypeRegistrations = new List<RegisterType>();
            RegisteredTypeRegistrations = new HashSet<int>();
            CurrentRunMode = runMode;
        }

        public void RegisterExtensionPoint(ExtensionPoint extensionPoint)
        {
            ExtensionPoints.Add(extensionPoint);
        }

        public void RegisterServiceContract(ServiceContract serviceContract)
        {
            ServiceContracts.Add(serviceContract);
        }

        public void RegisterType(RegisterType type)
        {
            switch (type.Role)
            {
                case RegistrationRole.DefaultImplementation:
                    //_logger.Info(_logCategory, "RegistrationsManager: Registering Type '{0}' as default implementation for '{1}' Service, Lifetime = {2}", type.ResolvingTypeName, type.Implements.Name, type.Lifetime);
                    break;
                case RegistrationRole.SimulatorImplementation:
                    //_logger.Info(_logCategory, "RegistrationsManager: Registering Type '{0}' as simulator implementation for '{1}' Service, Lifetime = {2}", type.ResolvingTypeName, type.Implements.Name, type.Lifetime);
                    break;
                case RegistrationRole.Extension:
                    //_logger.Info(_logCategory, "RegistrationsManager: Registering Extension '{0}' implementing '{1}' Extension Point, Lifetime = {2}", type.ResolvingTypeName, type.Implements.Name, type.Lifetime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type.Role");
            }

            TypeRegistrations.Add(type);
        }

        public void AutoRegisterAssembly(Assembly assembly)
        {
            var types = from type in assembly.GetTypes()
                        where !type.IsAbstract
                        select type;

            foreach (var type in types)
            {
                AutoRegisterTypeInternal(type);
            }
        }

        private void CollectExtensionPoints(Type type)
        {
            foreach (var attribute in type.GetAttributeList<ExtensionPoint>())
            {
                if (attribute.Contract == null)
                {
                    attribute.Contract = type;
                }

                RegisterExtensionPoint(attribute);
            }
        }

        private void AutoRegisterTypeInternal(Type type)
        {
            CollectExtensionPoints(type);
            CollectServiceContracts(type);

            CollectRegistrations(type);
        }

        private void CollectServiceContracts(Type type)
        {
            foreach (var attribute in type.GetAttributeList<ServiceContract>())
            {
                if (attribute.Contract == null)
                {
                    attribute.Contract = type;
                }

                RegisterServiceContract(attribute);
            }
        }

        private void AutoRegister(TypesRegistratorsManagerBase typesRegistratorsManager, IConfiguration configuration)
        {
            try
            {
                _startupRegistrators = typesRegistratorsManager.GetAllRegistrators();
                foreach (var r in _startupRegistrators)
                {
                    _logger?.Info($"Register using regiatrator {r.GetType().FullName}");
                    AutoRegisterAssembly(r.GetType().Assembly);
                }

                foreach (var r in _startupRegistrators)
                {
                    _logger?.Info($"PostRegister using regiatrator {r.GetType().FullName}");
                    r.ConfigureServices(this, Services, configuration, _logger);
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                string message = "Extensions Load Exceptions:\r\n";
                if (ex.LoaderExceptions != null)
                {
                    message = ex.LoaderExceptions.Aggregate(message,
                        (current, loaderException) =>
                            current + $"------\r\n{loaderException}\r\n--------\r\n");
                }

                _logger?.Critical(message, ex);

                throw;
            }
        }

        public void RegisterEndpoints(IEndpointRouteBuilder builder)
        {
            foreach (var r in _startupRegistrators)
            {
                _logger?.Info($"RegisterEndpoints using regiatrator {r.GetType().FullName}");
                r.UseEndpoints(builder, _logger);
            }
        }

        public void AutoRegisterUsingMefCatalog(ComposablePartCatalog catalog, IConfiguration configuration)
        {
            var mefRegistrator = new TypesRegistratorsManagerMef(catalog);
            AutoRegister(mefRegistrator, configuration);
        }

        public void AutoRegisterUsingReflection(IConfiguration configuration)
        {
            var registrator = new TypesRegistratorsManagerReflection();

            AutoRegister(registrator, configuration);
        }

        private void CollectRegistrations(Type type)
        {
            foreach (RegisterType attribute in type.GetAttributeList<RegisterType>())
            {
                if (attribute.TypeToRegister == null)
                {
                    attribute.TypeToRegister = type;
                }

                if (attribute.Implements == null)
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces?.Length > 0)
                    {
                        attribute.Implements = interfaces[0];

                    }
                    else
                    {
                        attribute.Implements = type;
                    }
                }

                RegisterType(attribute);
            }
        }

        public void CommitRegistrationsToContainer()
        {
            //RegisterExtensionPointsIntoContainer(_container);
            TypeRegistrations.Sort((t1, t2) => t1.ExtensionOrderPriority.CompareTo(t2.ExtensionOrderPriority));

            AdjustToRunMode();

            CheckNoCircularReferences();

            RegisterTypesIntoContainer();
        }

        private void RegisterTypesIntoContainer()
        {
            foreach (var registerAttribute in TypeRegistrations)
            {
                // skip registered
                if (RegisteredTypeRegistrations.Contains(RuntimeHelpers.GetHashCode(registerAttribute)))
                {
                    continue;
                }

                RegisteredTypeRegistrations.Add(RuntimeHelpers.GetHashCode(registerAttribute));

                Type from = registerAttribute.Implements;
                Type to = registerAttribute.TypeToRegister;

                switch (registerAttribute.Lifetime)
                {
                    case LifetimeManagement.Transient:
                        Services.AddTransientOnce(from, to);
                        break;
                    case LifetimeManagement.Singleton:
                        if (registerAttribute.InstanceToRegister != null)
                        {
                            Services.AddSingletonOnce(from, registerAttribute.InstanceToRegister);
                        }
                        else
                        {
                            Services.AddSingletonOnce(from, to);
                        }
                        break;
                    case LifetimeManagement.Scoped:
                        Services.AddScopedOnce(from, to);
                        break;
                }
            }
        }

        private void AdjustToRunMode()
        {
            if (CurrentRunMode == RunMode.Simulator)
            {
                IEnumerable<RegisterType> simulatorRegisterTypes =
                    TypeRegistrations.Where(tr => tr.Role == RegistrationRole.SimulatorImplementation);

                TypeRegistrations.RemoveAll(tr => tr.Role == RegistrationRole.DefaultImplementation && simulatorRegisterTypes.Any(srt => srt.Implements == tr.Implements));
            }
            else
            {
                TypeRegistrations.RemoveAll(tr => tr.Role == RegistrationRole.SimulatorImplementation);
            }
        }

        private void CheckNoCircularReferences()
        {
            Dictionary<Type, HashSet<Type>> typeToConstructorParams = new Dictionary<Type, HashSet<Type>>();

            foreach (var registerAttribute in TypeRegistrations)
            {
                ConstructorInfo[] constructorInfos = registerAttribute.TypeToRegister.GetConstructors();
                HashSet<Type> constructorParamTypes = new HashSet<Type>(constructorInfos.SelectMany(ci => ci.GetParameters().Select(p => p.ParameterType.IsArray ? p.ParameterType.GetElementType() : p.ParameterType)));

                foreach (RegisterType referencedRegisterType in TypeRegistrations.Where(t => constructorParamTypes.Any(p => p == t.Implements)))
                {
                    ConstructorInfo[] referencedConstructorInfos = referencedRegisterType.TypeToRegister.GetConstructors();
                    HashSet<Type> referencedConstructorParamTypes = new HashSet<Type>(referencedConstructorInfos.SelectMany(ci => ci.GetParameters().Select(p => p.ParameterType.IsArray ? p.ParameterType.GetElementType() : p.ParameterType)));

                    if (referencedConstructorParamTypes.Contains(registerAttribute.Implements))
                    {
                        throw new CircularClassReferenceException(registerAttribute.TypeToRegister, referencedRegisterType.TypeToRegister);
                    }
                }
            }
        }

        public T[] GetAllInstancesImplementingInterface<T>() where T : class
        {
            var instances = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(T)) && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => Activator.CreateInstance(t) as T)
                .ToArray();

            return instances;
        }

        public Type[] GetAllTypesImplementing<T>() where T : class
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => typeof(T) != t && typeof(T).IsAssignableFrom(t))
                .ToArray();

            return types;
        }

        public Assembly[] GetAssembliesContaining<T>()
        {
            return TypeRegistrations
                .Where(t => t.Implements == typeof(T))
                .Select(t => t.TypeToRegister.Assembly)
                .Distinct()
                .ToArray();
        }

        public Type[] GetTypesImplementing<T>()
        {
            return TypeRegistrations
                .Where(t => typeof(T).IsAssignableFrom(t.TypeToRegister))
                .Select(t => t.TypeToRegister)
                .ToArray();
        }
    }
}
