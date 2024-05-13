using backend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backend.components.middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class Authorization : Attribute, IAuthorizationFilter
{
    private readonly IList<UserType> _roles;

    public Authorization(params UserType[] _roles)
    {
        this._roles = _roles ?? new UserType[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isRolePermission = false;
        User user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            context.Result = new JsonResult(
                    new { Message = "Unauthorization" }
                )
                { StatusCode = StatusCodes.Status401Unauthorized };
        }

        if (user != null && this._roles.Any())
            foreach (var AuthRole in this._roles)
            {
                if (user.UserType == AuthRole)
                {
                    isRolePermission = true;
                }
            }

        if (!isRolePermission)
            context.Result = new JsonResult(
                    new { Message = "Unauthorization" }
                )
                { StatusCode = StatusCodes.Status401Unauthorized };
    }
}