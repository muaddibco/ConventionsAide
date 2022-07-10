using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication
{
    public class ConsumerPrincipalClaimsTransformation : IClaimsTransformation
    {
        private readonly IAuthenticationProducer _authenticationProducer;

        public ConsumerPrincipalClaimsTransformation(IAuthenticationProducer authenticationProducer)
        {
            _authenticationProducer = authenticationProducer;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            return await _authenticationProducer
                .ProduceConsumerPrincipalAsync(principal);
        }
    }
}
