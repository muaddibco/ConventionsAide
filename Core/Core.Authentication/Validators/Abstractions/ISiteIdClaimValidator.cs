using Microsoft.AspNetCore.Authorization;

namespace ConventionsAide.Core.Authentication.Validators.Abstractions
{
    public interface ISiteIdClaimValidator
    {
        bool IsValid(AuthorizationHandlerContext context);
    }
}
