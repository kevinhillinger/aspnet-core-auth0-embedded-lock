using Security.Authorization.Model;
using System.Collections.Generic;
using App.Security.Authorization.Model;
using System.Collections.Immutable;
using System.Security.Claims;

namespace App.Security.Authorization
{
    /// <summary>
    /// An anonymous authorization context, i.e. the user isn't authenticated!
    /// </summary>
    class AnonymousAuthorizationContext : IAuthorizationContext
    {
        public AuthorizationContextKey Key => AuthorizationContextKey.Anonymous();

        public ClaimsPrincipal Principal => new ClaimsPrincipal();

        public ImmutableHashSet<PermissionCode> Permissions => new HashSet<PermissionCode>().ToImmutableHashSet();

        public bool Has(PermissionCode permission)
        {
            return false;
        }

        public bool HasAll(IEnumerable<PermissionCode> permissions)
        {
            return false;
        }

        public bool HasAny(IEnumerable<PermissionCode> permissions)
        {
            return false;
        }
    }
}
