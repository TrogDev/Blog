namespace Blog.Web.Common.Extensions;

using System.Security.Claims;

using Blog.Domain.Enums;

public static class UserProviderExtension
{
    public static long GetUserId(this ClaimsPrincipal user)
    {
        return long.Parse(user.FindFirstValue("Id"));
    }

    public static Role GetUserRole(this ClaimsPrincipal user)
    {
        return Enum.Parse<Role>(user.FindFirstValue("Role"));
    }
}