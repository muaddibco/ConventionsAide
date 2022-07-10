using ConventionsAide.Core.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConventionsAide.Core.Common.Architecture.Registration
{
    internal class TypesRegistratorsManagerReflection : TypesRegistratorsManagerBase
    {
        protected override IEnumerable<StartupRegistratorBase> GetTypeRegistrators()
        {
            return ReflectionHelper
                .FindTypes(IsRegistratorType)
                .Select(CreateRegistratorInstance)
                .ToList();
        }

        private static bool IsRegistratorType(Type type)
        {
            return
                type is not null &&
                !type.IsAbstract &&
                typeof(StartupRegistratorBase).IsAssignableFrom(type);
        }

        private static StartupRegistratorBase CreateRegistratorInstance(Type registratorType)
        {
            var instance = Activator
                .CreateInstance(registratorType);

            return (StartupRegistratorBase)instance;
        }
    }
}
