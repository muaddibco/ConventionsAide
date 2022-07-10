using System;
using System.Security.Claims;
using ConventionsAide.Core.Authentication.Constants;

namespace ConventionsAide.Core.Authentication
{
    public static class PrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.Identity.Name;
        }

        public static Guid GetAntiForgeryToken(this ClaimsPrincipal user)
        {
            var principal = user as ConsumerPrincipal;

            if (principal == null)
            {
                return Guid.Empty;
            }

            return principal.UserData.AntiForgeryToken
                .GetValueOrDefault(Guid.Empty);
        }

        public static bool IsGuest(this ClaimsPrincipal user)
        {
            return user.IsInRole(RoleNames.Guest);
        }
    }
}
