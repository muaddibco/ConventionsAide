using ConventionsAide.Core.Common.Architecture;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PostSharp.Patterns.Contracts;
using System;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication
{
    /// <summary>
    /// A default implementation for the authentication context.
    /// </summary>
    [ScopedService]
    public class AuthenticationContext : IAuthenticationContext
    {
        private readonly ConcurrentDictionary<string, JwtSecurityToken> _apiTokens = new();
        private readonly IApiAuthorizationProvider _apiAuthorizationProvider;
        private readonly IAuthenticationProducer _authenticationProducer;
        private readonly IConfiguration _configuration;
        private string _apiToken;

        public AuthenticationContext(IApiAuthorizationProvider apiAuthorizationProvider, IAuthenticationProducer authenticationProducer, IConfiguration configuration)
        {
            _apiAuthorizationProvider = apiAuthorizationProvider;
            _authenticationProducer = authenticationProducer;
            _configuration = configuration;
        }

        /// <inheritdoc />
        public ConsumerPrincipal User { get; private set; }

        public void SetUserFromHeader(byte[] content)
        {

            var consumerPrincipal = _authenticationProducer.Deserialize(content);
            User = consumerPrincipal;
        }

        public async Task<string> FetchApiToken([NotNull]string apiName)
        {
            if(!_apiTokens.TryGetValue(apiName, out JwtSecurityToken token))
            {
                token = await ObtainAndCacheToken(apiName);
            }

            if (DateTime.Now < token.ValidFrom || DateTime.Now > token.ValidTo)
            {
                token = await ObtainAndCacheToken(apiName);
            }

            return token.RawData;
        }

        public void StoreApiToken(string apiToken)
        {
            _apiToken = apiToken;
        }

        public async Task ValidateApiToken(string audience, string scope)
        {
            SecurityToken validatedToken = await ValidateAudience(audience);

            ValidateScope(scope, validatedToken);
        }

        private async Task<SecurityToken> ValidateAudience(string audience)
        {
            string issuer = _configuration["AuthApi:Issuer"];

            IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{issuer}.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(_apiToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = issuer,
                ValidAudience = $"https://{audience}Api/",
                IssuerSigningKeys = openIdConfig.SigningKeys
            }, out SecurityToken validatedToken);
            return validatedToken;
        }

        private static void ValidateScope(string scope, SecurityToken validatedToken)
        {
            if (!scope.IsNullOrEmpty())
            {
                var jwtToken = validatedToken as JwtSecurityToken;
                var permissionsValue = jwtToken.Payload["permissions"]?.ToString();
                var permissions = JsonConvert.DeserializeObject<string[]>(permissionsValue);
                if (!permissions.Contains(scope))
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        private async Task<JwtSecurityToken> ObtainAndCacheToken(string apiName)
        {
            var tokenString = await _apiAuthorizationProvider.ObtainTokenAsync(apiName);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;
            _apiTokens.AddOrUpdate(apiName, token, (k, v) => token);
            return token;
        }
    }
}
