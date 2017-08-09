using App.Security.Authorization.Model;
using Security.Authorization.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace App.Security.Authorization.Data
{
    public interface IPermissionSetCache
    {
        bool Set(AuthorizationContextKey key, ISet<PermissionCode> value, TimeSpan? expiresIn = null);

        ImmutableHashSet<PermissionCode> Get(AuthorizationContextKey key);

        bool Exists(AuthorizationContextKey key);

        void Remove(AuthorizationContextKey key);
    }
}
