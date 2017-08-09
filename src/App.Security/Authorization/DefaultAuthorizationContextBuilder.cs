using System.Security.Claims;
using Security.Authorization.Model;
using App.Security.Authorization.Model;
using System.Collections.Generic;
using System.Linq;
using App.Security.Authorization.Data;

namespace App.Security.Authorization
{
    class DefaultAuthorizationContextBuilder : IAuthorizationContextBuilder
    {
        private readonly IPermissionSetProvider permissionsProvider;

        public DefaultAuthorizationContextBuilder(IPermissionSetProvider permissionsProvider)
        {
            this.permissionsProvider = permissionsProvider;
        }

        public IAuthorizationContext Build(ClaimsPrincipal principal)
        {
            if (principal == null)
                return new AnonymousAuthorizationContext();

            var key = GetKey(principal.Claims);
            
            var permissions = permissionsProvider.GetEffectivePermissionSet(key);

            return new DefaultAuthorizationContext(key, principal, permissions);
        }

        private AuthorizationContextKey GetKey(IEnumerable<Claim> claims)
        {
            var isAnonymous = claims == null || (!claims.Any(UserId) || !claims.Any(TenantId));

            if (isAnonymous)
                return AuthorizationContextKey.Anonymous();

            return AuthorizationContextKey.Create(claims.First(UserId).Value, claims.First(TenantId).Value);
        }

        private bool UserId(Claim claim)
        {
            return claim.Type == SecurityClaimTypes.UserId;
        }

        private bool TenantId(Claim claim)
        {
            return claim.Type == SecurityClaimTypes.TenantId;
        }
    }
}
