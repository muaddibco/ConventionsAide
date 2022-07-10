using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ConventionsAide.Core.Authentication.Constants;
using ConventionsAide.Core.Common.Architecture;
using Microsoft.Extensions.Configuration;

namespace ConventionsAide.Core.Authentication
{
    [RegisterService(typeof(IAuthenticationProducer), Lifetime = LifetimeManagement.Scoped)]
    public class AuthenticationProducer : IAuthenticationProducer
    {
        private readonly AuthOptions _authOptions;

        public AuthenticationProducer(IConfiguration configuration)
        {
            _authOptions = configuration
                .GetSection(AuthOptions.Name)
                .Get<AuthOptions>();
        }

        public byte[] Serialize(ConsumerPrincipal principal)
        {
            using MemoryStream memoryStream = new ();
            using BinaryWriter binaryWriter = new (memoryStream);

            principal.Principal.WriteTo(binaryWriter);

            return memoryStream
                .ToArray();
        }

        public async Task<ConsumerPrincipal> Deserialize(byte[] source)
        {
            if (source == null)
            {
                return null;
            }

            using MemoryStream memoryStream = new (source);
            using BinaryReader binaryReader = new (memoryStream);

            var principal = new ClaimsPrincipal(binaryReader);

            return await ProduceConsumerPrincipalAsync(principal);
        }

        public async Task<ConsumerPrincipal> ProduceConsumerPrincipalAsync(ClaimsPrincipal principal)
        {
            string[] userRoles = HandleRoleClaims(principal);

            var userdata = new PrincipalUserData
            {
                Roles = userRoles
            };

            var userName = principal.GetUserName() ?? string.Empty;

            var genericIndentity = new GenericIdentity(userName);
            genericIndentity.AddClaims(principal.Claims);

            var consumerPrincipal = new ConsumerPrincipal(genericIndentity, userRoles, userdata, principal);

            return consumerPrincipal;
        }

        private string[] HandleRoleClaims(ClaimsPrincipal principal)
        {
            string[] userRoles = principal
               .FindAll(ClaimTypes.Role)
               ?.Select(c => c.Value)
               ?.Distinct()
               ?.ToArray();

            if (userRoles != null && userRoles.Any())
            {
                return userRoles;
            }

            // TODO: This line is added in order to fill user role.
            // Since only member is able to login, then 'Member' or 'Guest' role is set for user depending on member ID.
            // Adjust when more roles will be supported.
            return new[] { RoleNames.Guest };
        }
    }
}
