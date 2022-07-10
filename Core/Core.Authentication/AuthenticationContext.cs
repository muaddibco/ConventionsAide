using ConventionsAide.Core.Common.Architecture;
using Microsoft.IdentityModel.Tokens;
using PostSharp.Patterns.Contracts;
using System;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
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
        private string _apiToken;

        public AuthenticationContext(IApiAuthorizationProvider apiAuthorizationProvider, IAuthenticationProducer authenticationProducer)
        {
            _apiAuthorizationProvider = apiAuthorizationProvider;
            _authenticationProducer = authenticationProducer;
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

        public void ValidateApiToken(string audience, string scope)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(_apiToken, new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = audience
            }, out SecurityToken validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;
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
