using System.IO;
using System.Reflection;
using ConventionsAide.Core.Common;
using Microsoft.Extensions.Configuration;

namespace ConventionsAide.Core.Services
{
    public class ConfigBuilder
    {
        private readonly string _environment;

        public ConfigBuilder(string env)
        {
            _environment = env;
        }

        public IConfigurationBuilder SetupConfiguration(IConfigurationBuilder config)
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            config.AddJsonFile(path: Path.Combine(currentPath, "Shared", $"{EnvironmentConsts.SharedSettingFileName}.json"), optional: true)
            .AddJsonFile(path: Path.Combine(currentPath, "Shared", $"{EnvironmentConsts.SharedSettingFileName}.{_environment}.json"), optional: true)
            .AddJsonFile(path: "appsettings.json")
            .AddJsonFile($"appsettings.{_environment}.json", optional: true)
             .AddEnvironmentVariables();
             //.AddAzureKeyVault(environment: _environment); // must be loaded last
            return config;
        }
    }
}
