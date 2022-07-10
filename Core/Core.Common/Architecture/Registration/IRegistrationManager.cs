using System;
using System.Reflection;


namespace ConventionsAide.Core.Common.Architecture.Registration
{
    public interface IRegistrationManager
    {
        void AutoRegisterAssembly(Assembly assembly);
        Assembly[] GetAssembliesContaining<T>();
        Type[] GetTypesImplementing<T>();
        Type[] GetAllTypesImplementing<T>() where T : class;
    }
}
