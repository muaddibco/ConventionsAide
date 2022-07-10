using ConventionsAide.Core.Common.Architecture;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication
{
    [ServiceContract]
    public interface IAuthenticationProducer
    {
        ConsumerPrincipal ProduceConsumerPrincipal(ClaimsPrincipal principal);

        byte[] Serialize(ClaimsPrincipal principal);

        ConsumerPrincipal Deserialize(byte[] source);
    }
}
