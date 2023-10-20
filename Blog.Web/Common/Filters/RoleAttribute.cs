namespace Blog.Web.Common.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Blog.Domain.Enums;
using Blog.Web.Common.Extensions;
using Blog.Domain.Entities;

public class RoleAttribute : ActionFilterAttribute
{
    private readonly Role[] roles;

    public RoleAttribute(params Role[] roles)
    {
        this.roles = roles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;
        if (user is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        if (!roles.Contains(user.GetUserRole()))
        {
            context.Result = new ForbidResult();
            return;
        }
        base.OnActionExecuting(context);
    }
}