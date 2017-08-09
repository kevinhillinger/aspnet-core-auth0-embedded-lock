using App.Security.Authorization.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Text;

namespace Security.Authorization.Model
{
    /// <summary>
    /// Represents an authorization context to perform checks against
    /// </summary>
    public interface IAuthorizationContext
    {
        /// <summary>
        /// Gets the key for the authorization context
        /// </summary>
        AuthorizationContextKey Key { get; }
        
        ClaimsPrincipal Principal { get; }

        ImmutableHashSet<PermissionCode> Permissions { get; }

        /// <summary>
        /// Checks if this context has the permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool Has(PermissionCode permission);

        /// <summary>
        /// Checks whether this context has all the provided permissions (AND gate)
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        bool HasAll(IEnumerable<PermissionCode> permissions);

        /// <summary>
        /// Checks whether the context has any of the demanded permissions (OR gate)
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        bool HasAny(IEnumerable<PermissionCode> permissions);
    }
}
