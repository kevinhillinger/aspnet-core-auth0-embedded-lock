using App.Security.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace App.Security.AspNet.Authorization
{
    /// <summary>
    /// The authorization handler
    /// </summary>
    public class DefaultPermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IAuthorizationContextBuilder authorizationContextBuilder;

        public DefaultPermissionHandler(IAuthorizationContextBuilder authorizationContextBuilder)
        {
            this.authorizationContextBuilder = authorizationContextBuilder;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var authorizationContext = authorizationContextBuilder.Build(context.User);

            if (authorizationContext.Has(requirement.PermissionCode))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
