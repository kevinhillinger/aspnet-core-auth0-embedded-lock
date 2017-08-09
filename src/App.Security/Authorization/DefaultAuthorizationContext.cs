using Security.Authorization.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;

namespace App.Security.Authorization.Model
{
    /// <summary>
    /// The default implementation of the authorization context
    /// </summary>
    class DefaultAuthorizationContext : IAuthorizationContext
    {
        public AuthorizationContextKey Key { get; }

        public ClaimsPrincipal Principal { get; }

        public ImmutableHashSet<PermissionCode> Permissions { get; }

        public DefaultAuthorizationContext(AuthorizationContextKey key, ClaimsPrincipal principal, ImmutableHashSet<PermissionCode> permissions)
        {
            Key = key;
            Principal = principal;
            Permissions = permissions;
        }

        public bool Has(PermissionCode permission)
        {
            return Permissions.Contains(permission);
        }

        public bool HasAll(IEnumerable<PermissionCode> permissions)
        {
            return Permissions.Intersect(permissions).Count == permissions.Count();
        }

        public bool HasAny(IEnumerable<PermissionCode> permissions)
        {
            return Permissions.Intersect(permissions).Count > 0;
        }
    }
}
