using App.Security.Authorization.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Authorization.Model;
using System.Collections.Generic;

namespace App.Security.AspNet.Authorization.Extensions
{
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// Applies each permission as an asp.net core policy that can then be authorized using <see cref="PermissionRequirement"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="permissions"></param>
        public static void AddPermissionPolicies(this IServiceCollection services, IEnumerable<Permission> permissions)
        {
            var builder = new AuthorizationBuilder(permissions);

            services.AddAuthorization(options =>
            {
                builder.Build(c =>
                {
                    // applies each permission as an asp.net core policy that can then be authorized
                    foreach (var permission in c.Permissions)
                    {
                        var requirement = new PermissionRequirement(permission.Code);
                        options.AddPolicy(permission.Code, policy => policy.Requirements.Add(requirement));
                    }
                });
            });
        }
    }
}
