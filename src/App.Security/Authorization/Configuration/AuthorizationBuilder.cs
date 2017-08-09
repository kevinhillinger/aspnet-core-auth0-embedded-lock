using Security.Authorization.Model;
using System;
using System.Collections.Generic;

namespace App.Security.Authorization.Configuration
{
    /// <summary>
    /// An infrastructure startup builder to do things like get all permission codes
    /// </summary>
    public sealed class AuthorizationBuilder
    {
        private readonly IEnumerable<Permission> permissions;

        public AuthorizationBuilder(IEnumerable<Permission> permissions)
        {
            this.permissions = permissions;
        }

        public void Build(Action<AuthorizationConfigure> configure)
        {
            var config = new AuthorizationConfigure { Permissions = permissions };
            configure(config);
        }
    }
}
