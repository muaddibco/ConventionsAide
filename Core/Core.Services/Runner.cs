using System;
using ConventionsAide.Core.Common;
using ConventionsAide.Core.Common.Architecture;
using ConventionsAide.Core.Common.Aspects;
using ConventionsAide.Core.Common.ExtensionMethods;
using ConventionsAide.Core.Logging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace ConventionsAide.Core.Services;

public class Runner<TBootstrapper, TService>
    where TBootstrapper : Bootstrapper, new()
    where TService : BackgroundService
{
    private readonly string _applicationName;
    private readonly string _environment;
    private readonly ConfigBuilder _configBuilder;

    public Runner(string applicationName, string environmentVar)
    {
        _applicationName = applicationName;
        _environment = Environment.GetEnvironmentVariable(environmentVar);
        _configBuilder = new ConfigBuilder(_environment);
    }

    private IConfigurationRoot Config { get; set; }

    public static ILogger Logger { get; private set; }

    public void Start<TProgram>(string[] args)
        where TProgram : class
    {
        Config = _configBuilder
            .SetupConfiguration(new ConfigurationBuilder())
            .Build();

        ConfigSettingLayoutRenderer.DefaultConfiguration = Config;

        ILoggerFactory loggerFactory = LoggerFactory
            .Create(b => b
                .AddNLog(configFileName: GetNlogConfigurationFilePath())
                .AddConsole()
            );

        Logger = loggerFactory.CreateLogger<TProgram>();

        try
        {
            Logger.Info($"{_applicationName} Application Start");
            var host = CreateHostBuilder(args).Build();
            AspectServiceLocator.Initialize(host);
            host.Run();
        }
        catch (Exception ex)
        {
            //NLog: catch setup errors
            Logger.Critical($"{_applicationName} Stopped program because of exception", ex);
            throw;
        }
        finally
        {
            Logger.Info($"{_applicationName} Application Shutdown");
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
        }
    }

    public virtual string GetNlogConfigurationFilePath()
    {
        return $"nlog.{_environment}.config";
    }

    private IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddBootstrapper<TBootstrapper>(hostContext.Configuration, Logger);
            services.AddHostedService<TService>();
        })
        .ConfigureAppConfiguration((context, config) =>
        {
            _configBuilder.SetupConfiguration(config);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.Configure(app =>
            {
                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    app.ApplicationServices.GetRequiredService<TBootstrapper>().RegisterEndpoints(endpoints);
                });
            });
        })
        .UseNLog();
}

public class Runner : Runner<Bootstrapper, Service>
{
    public Runner(string applicationName, string environmentVar = EnvironmentConsts.UseAspCoreEnv) : base(applicationName, environmentVar)
    {
    }
}