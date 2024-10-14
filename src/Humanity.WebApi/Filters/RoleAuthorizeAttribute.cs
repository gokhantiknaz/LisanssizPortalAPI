using Microsoft.AspNetCore.Authorization;

namespace Humanity.WebApi.Filters;

public class RoleAuthorizeAttribute : AuthorizeAttribute
{
    public RoleAuthorizeAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}
