using Security.Authorization.Model;
using System.Security.Claims;

namespace App.Security.Authorization
{
    public interface IAuthorizationContextBuilder
    {
        IAuthorizationContext Build(ClaimsPrincipal principal);
    }
}
