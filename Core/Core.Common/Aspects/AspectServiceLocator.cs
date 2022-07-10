using Microsoft.Extensions.Hosting;
using System;

namespace ConventionsAide.Core.Common.Aspects
{
    public static class AspectServiceLocator
    {
        private static IServiceProvider _serviceProvider;

        public static void Initialize(IHost host)
        {
            _serviceProvider = host.Services;
        }

        public static T GetService<T>() where T : class
        {
            return GetServiceImpl<T>();
        }

        private static T GetServiceImpl<T>()
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException();
            }

            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
