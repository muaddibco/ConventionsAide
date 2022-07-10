using ConventionsAide.Core.Authentication;
using ConventionsAide.Core.Common;
using ConventionsAide.Core.Common.ExtensionMethods;
using ConventionsAide.Core.HealthChecks.ExtentionMethods;
using ConventionsAide.Core.Services;
using ConventionsAideGW;
using ConventionsAideGW.Middlewares;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri(builder.Configuration["OAuthServer:TokenUrl"]),
                AuthorizationUrl = new Uri(builder.Configuration["OAuthServer:AuthorizationUrl"]),
                Scopes = new Dictionary<string, string>
                            {
                                { "openid", "openid" },
                                { "profile", "profile" },
                                { "email", "email" },
                            },
            },
        },
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2",
                            },
                            Scheme = "oauth2",
                            Name = "oauth2",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
});

builder.Services.AddConsumerAuthentication(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateConventions", policy => policy.RequireClaim("permissions", "create:conventions"));
    options.AddPolicy("UpdateConventions", policy => policy.RequireClaim("permissions", "update:conventions"));
    options.AddPolicy("DeleteConventions", policy => policy.RequireClaim("permissions", "delete:conventions"));
    options.AddPolicy("ReadConventions", policy => policy.RequireClaim("permissions", "read:conventions"));
    options.AddPolicy("ReadVenues", policy => policy.RequireClaim("permissions", "read:venues"));
    options.AddPolicy("SyncVenues", policy => policy.RequireClaim("permissions", "sync:venues"));
    options.AddPolicy("CreateVenuesConfirmationFlows", policy => policy.RequireClaim("permissions", "create:venuesConfirmationFlows"));
    options.AddPolicy("UpdateVenuesConfirmationFlows", policy => policy.RequireClaim("permissions", "update:venuesConfirmationFlows"));
    options.AddPolicy("ReadVenuesConfirmationFlows", policy => policy.RequireClaim("permissions", "read:venuesConfirmationFlows"));
    options.AddPolicy("UpdateVenues", policy => policy.RequireClaim("permissions", "update:users"));
    options.AddPolicy("ReadUsers", policy => policy.RequireClaim("permissions", "update:users"));
    options.AddPolicy("CreateInvitations", policy => policy.RequireClaim("permissions", "create:invitations"));
    options.AddPolicy("UpdateInvitations", policy => policy.RequireClaim("permissions", "update:invitations"));
    options.AddPolicy("ReadInvitations", policy => policy.RequireClaim("permissions", "read:invitations"));
    options.AddPolicy("CreateRegistrations", policy => policy.RequireClaim("permissions", "create:registrations"));
    options.AddPolicy("UpdateRegistrations", policy => policy.RequireClaim("permissions", "update:registrations"));
    options.AddPolicy("ReadRegistrations", policy => policy.RequireClaim("permissions", "read:registrations"));
    options.AddPolicy("ReadInvitationRegistrations", policy => policy.RequireClaim("permissions", "read:invitationregistrations"));
});


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
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(app.Configuration["Secrets:OAuthServer:ClientId"]);
        c.OAuthClientSecret(app.Configuration["Secrets:OAuthServer:ClientSecret"]);
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        c.UseRequestInterceptor("(req) => { if (req.url.endsWith('oauth/token') && req.body) req.body += '&audience=" + app.Configuration["AuthSettings:Audience"] + "'; return req; }");
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseCors();

app.UseAuthentication();
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