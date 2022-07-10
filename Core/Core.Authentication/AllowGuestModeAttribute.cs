using System;
using System.Security.Principal;
using ConventionsAide.Core.Authentication.Constants;
using ConventionsAide.Core.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConventionsAide.Core.Authentication;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
public class AllowGuestModeAttribute : ActionFilterAttribute, IAllowAnonymous
{
    private const string SiteIdHeader = "SiteId";

    /// <inheritdoc/>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //if (HasMemberId(context.HttpContext.User))
        //{
        //    base.OnActionExecuting(context);
        //    return;
        //}

        try
        {
            var userName = "guest";

            var principal = context.HttpContext.User;
            var userdata = new PrincipalUserData
            {
                Roles = new[] { RoleNames.Guest }
            };

            var genericIdentity = new GenericIdentity(userName);
            genericIdentity.AddClaims(principal.Claims);

            var consumerPrincipal = new ConsumerPrincipal(genericIdentity, userdata?.Roles, userdata, principal);
            context.HttpContext.User = consumerPrincipal;
        }
        catch (SiteIdNotValidException ex)
        {
            context.Result = new BadRequestObjectResult(ex.Message);
        }

        base.OnActionExecuting(context);
    }
}
