using ConventionsAide.Core.Common;
using ConventionsAide.Core.Common.ExtensionMethods;
using ConventionsAide.Core.HealthChecks.ExtentionMethods;
using ConventionsAide.Core.Services;
using ConventionsAideGW;
using ConventionsAideGW.Middlewares;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;

CancellationTokenSource cancellationTokenSource = new(); 
var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable(EnvironmentConsts.UseAspCoreEnv);
var configBuilder = new ConfigBuilder(environment);
IConfigurationRoot config = configBuilder.SetupConfiguration(builder.Configuration).Build();

ConfigSettingLayoutRenderer.DefaultConfiguration = config;

ILoggerFactory loggerFactory = LoggerFactory.Create(b => b.AddNLog(configFileName: $"nlog.{builder.Environment.EnvironmentName}.config").AddConsole());
ILogger logger = loggerFactory.CreateLogger<Program>();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .SetIsOriginAllowed(IsOriginAllowed)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
        options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
    });

builder.Services.AddSwaggerGenNewtonsoftSupport();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBootstrapper<ApiGwBootstrapper>(builder.Configuration, logger);

// HealthChecks
builder.Services.AddHealthChecks();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.Lifetime.ApplicationStopping.Register(cancellationTokenSource.Cancel);
await app.Services.UseBootstrapper<ApiGwBootstrapper>(cancellationTokenSource.Token, logger);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseCors();

app.UseAuthorization();
app.UseCorrelationIdInjector();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecksEndPoint();
});

app.Run();

bool IsOriginAllowed(string origin)
{
    try
    {
        if (string.IsNullOrEmpty(origin))
        {
            return false;
        }
        
        var allowedHosts = builder.Configuration.GetSection("AllowedCorsHosts").Get<string[]>();
        if (allowedHosts == null)
        {
            return false;
        }

        var originUri = new Uri(origin);
        return allowedHosts.Any(host => originUri.Host.Equals(host));
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"Failed to check origin {origin}.");
        return false;
    }
}