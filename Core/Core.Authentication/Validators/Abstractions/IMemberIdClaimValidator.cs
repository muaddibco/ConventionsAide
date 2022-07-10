using Microsoft.AspNetCore.Authorization;

namespace ConventionsAide.Core.Authentication.Validators.Abstractions
{
    public interface IMemberIdClaimValidator
    {
        bool IsValid(AuthorizationHandlerContext context);
    }
}
