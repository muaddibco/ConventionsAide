using ConventionsAide.Core.Authentication.Constants;
using ConventionsAide.Core.Authentication.Validators.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace ConventionsAide.Core.Authentication.Validators
{
    public class MemberIdClaimValidator : IMemberIdClaimValidator
    {
        public bool IsValid(AuthorizationHandlerContext context)
        {
            return context.User.HasClaim(claim =>
                claim.Type == ClaimNames.MemberId &&
                claim.Value != "0");
        }
    }
}
