using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Core.Services
{
    public  interface IHttpService
    {
        string GetUserName();

        Guid GetTenantId();

        public ClaimsIdentity GetIdentity();
    }
}
