using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConventionsAide.Core.Common.ApiNonActionHelpers;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
public class CustomNonActionAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (IfDisabledLogic(context))
        {
            context.Result = new NotFoundResult();
        }
        else
        {
            base.OnActionExecuting(context);
        }
    }

    bool IfDisabledLogic(ActionExecutingContext context)
    {
        //here we can use feature flag from db or configuration.
        return true;
    }
}
