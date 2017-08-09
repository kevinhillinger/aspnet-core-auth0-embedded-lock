using App.Security.Authorization.Model;
using System.Collections.Immutable;

namespace App.Security.Authorization
{
    public interface IPermissionSetProvider
    {
        /// <summary>
        /// Gets the set of permissions that are effective for a particular authz context key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ImmutableHashSet<PermissionCode> GetEffectivePermissionSet(AuthorizationContextKey key);

        /// <summary>
        /// Clears the current permission set with the authorization context key as the lookup value
        /// </summary>
        /// <param name="key"></param>
        void Clear(AuthorizationContextKey key);
    }
}
