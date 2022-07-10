using ConventionsAide.Core.Authentication.Validators.Abstractions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ConventionsAide.Core.Authentication.Policies.Default
{
    public class MandatoryClaimsAuthorizationHandler : AuthorizationHandler<MandatoryClaimsRequirement>
    {
        private readonly ISiteIdClaimValidator _siteIdClaimValidator;
        private readonly IMemberIdClaimValidator _memberIdClaimValidator;

        public MandatoryClaimsAuthorizationHandler(
            ISiteIdClaimValidator siteIdClaimValidator,
            IMemberIdClaimValidator memberIdClaimValidator)
        {
            _siteIdClaimValidator = siteIdClaimValidator;
            _memberIdClaimValidator = memberIdClaimValidator;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MandatoryClaimsRequirement requirement)
        {
            if (_memberIdClaimValidator.IsValid(context) &&
                _siteIdClaimValidator.IsValid(context))
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
