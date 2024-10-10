using Humanity.Application;
using Humanity.Application.Core.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Humanity.Infrastructure.Services
{
    public class HttpUserService(IHttpContextAccessor httpContextAccessor) : IHttpService
    {
        public Guid GetTenantId()
        {
            var tenantIdString = httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.GroupSid);
            if (Guid.TryParse(tenantIdString, out var tenantId))
            {
                return tenantId;
            }

            return Guid.Empty;
        }

        public string GetUserName() => httpContextAccessor.HttpContext.User.Identity.Name;

        public ClaimsIdentity GetIdentity() => httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
    }

}
