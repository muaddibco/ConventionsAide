using ConventionsAide.Core.Common.Architecture;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ConventionsAide.Core.Authentication
{
    [TransientService]
    public class ApiAuthorizationProvider : IApiAuthorizationProvider
    {
        private readonly IConfiguration _configuration;

        public ApiAuthorizationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ObtainTokenAsync(string apiName)
        {
            string tokenUrl = _configuration[$"Auth{apiName}Api:TokenUrl"];
            string clientId = _configuration[$"Secrets:Auth{apiName}Api:ClientId"];
            string clientSecret = _configuration[$"Secrets:Auth{apiName}Api:ClientSecret"];

            if(tokenUrl.IsNullOrEmpty())
            {
                throw new ArgumentException($"No value provided for Auth{apiName}Api:TokenUrl");
            }
            if (clientId.IsNullOrEmpty())
            {
                throw new ArgumentException($"No value provided for Secrets:Auth{apiName}Api:ClientId");
            }
            if (clientSecret.IsNullOrEmpty())
            {
                throw new ArgumentException($"No value provided for Secrets:Auth{apiName}Api:ClientSecret");
            }

              var response = await tokenUrl
                .PostJsonAsync(
                    new AuthorizationRequestBody
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret,
                        Audience = $"https://{apiName}/"
                    })
                .ReceiveJson<AuthorizationResponse>();

            return response.AccessToken;
        }

        internal class AuthorizationRequestBody
        {
            private const string _defaultGrantType = "client_credentials";

            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string Audience { get; set; }
            public string GrantType { get; set; } = _defaultGrantType;
        }

        internal class AuthorizationResponse
        {
            public string AccessToken { get; set; }
            public string Scope { get; set; }
            public int ExpiresIn { get; set; }
            public string TokenType { get; set; }
        }
    }
}
