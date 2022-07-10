using ConventionsAide.Core.Common.Architecture;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication
{
    [ServiceContract]
    public interface IAuthenticationProducer
    {
        Task<ConsumerPrincipal> ProduceConsumerPrincipalAsync(ClaimsPrincipal principal);

        byte[] Serialize(ConsumerPrincipal principal);

        Task<ConsumerPrincipal> Deserialize(byte[] source);
    }
}
