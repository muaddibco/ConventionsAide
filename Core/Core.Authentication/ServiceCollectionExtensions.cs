using ConventionsAide.Core.Authentication.Constants;
using ConventionsAide.Core.Authentication.Policies.Default;
using ConventionsAide.Core.Authentication.Policies.MemberRegistration;
using ConventionsAide.Core.Authentication.Validators;
using ConventionsAide.Core.Authentication.Validators.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConsumerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration
                .GetSection(AuthOptions.Name)
                .Get<AuthOptions>();

            var isM2M = authOptions.IsM2M.HasValue && authOptions.IsM2M.Value;

            if (string.IsNullOrEmpty(authOptions.UsernameClaimType) && !isM2M)
            {
                throw new ArgumentNullException(
                    paramName: nameof(authOptions.UsernameClaimType),
                    message: $"Mandatory configuration parameter {nameof(authOptions.UsernameClaimType)} is missing");
            }

            if (string.IsNullOrEmpty(authOptions.SiteIdClaimType) && !isM2M)
            {
                throw new ArgumentNullException(
                    paramName: nameof(authOptions.SiteIdClaimType),
                    message: $"Mandatory configuration parameter {nameof(authOptions.SiteIdClaimType)} is missing");
            }

            services
                .AddHttpContextAccessor()
                .AddScoped<IClaimsTransformation, ConsumerPrincipalClaimsTransformation>()
                .AddValidators()
                .AddAuthorizationHandlers()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = authOptions.Authority;// "https://dev-67923645.okta.com/oauth2/default";
                    options.TokenValidationParameters.ValidAudience = authOptions.Audience;
                    if (!isM2M)
                    {
                        options.TokenValidationParameters.NameClaimType = authOptions.UsernameClaimType;
                    }
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = async c =>
                        {
                           await Task.CompletedTask;
                        },
                        OnForbidden = async c =>
                        {
                          await Task.CompletedTask;
                        },
                        OnTokenValidated = async c =>
                        {
                           await Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddMemberRegistrationPolicy();

                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .AddRequirements(new MandatoryClaimsRequirement())
                .Build();
            });
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMemberIdClaimValidator, MemberIdClaimValidator>()
                .AddSingleton<ISiteIdClaimValidator, SiteIdClaimValidator>();
        }

        private static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAuthorizationHandler, MandatoryClaimsAuthorizationHandler>()
                .AddSingleton<IAuthorizationHandler, MemberRegistrationAuthorizationHandler>();
        }

        private static void AddMemberRegistrationPolicy(this AuthorizationOptions options)
        {
            var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .AddRequirements(new MemberRegistrationClaimsRequirement())
                .Build();

            options.AddPolicy(AuthorizationPolicyNames.MemberRegistrationPolicy, policy);
        }
    }
}
