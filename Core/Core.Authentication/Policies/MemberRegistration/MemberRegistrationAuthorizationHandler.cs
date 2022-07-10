using ConventionsAide.Core.Authentication.Validators.Abstractions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication.Policies.MemberRegistration
{
    public class MemberRegistrationAuthorizationHandler : AuthorizationHandler<MemberRegistrationClaimsRequirement>
    {
        private readonly ISiteIdClaimValidator _siteIdClaimValidator;

        public MemberRegistrationAuthorizationHandler(ISiteIdClaimValidator siteIdClaimValidator)
        {
            _siteIdClaimValidator = siteIdClaimValidator;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MemberRegistrationClaimsRequirement requirement)
        {
            if (_siteIdClaimValidator.IsValid(context))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
