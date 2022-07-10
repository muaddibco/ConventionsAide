using System.Security.Claims;
using System.Security.Principal;

namespace ConventionsAide.Core.Authentication
{
    public class ConsumerPrincipal : GenericPrincipal
    {
        public ConsumerPrincipal(IIdentity identity, string[] roles, PrincipalUserData userData, ClaimsPrincipal principal) : base(identity, roles)
        {
            UserData = userData;
            Principal = principal;
        }

        public PrincipalUserData UserData { get; }

        internal ClaimsPrincipal Principal { get; }
    }
}