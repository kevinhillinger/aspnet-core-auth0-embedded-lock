using App.Security.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace App.Security.AspNet.Authorization
{
    public class AuthorizationCookieAuthEventSubscriber
    {
        private readonly IPermissionSetProvider permissionSetProvider;
        private readonly IAuthorizationContextBuilder authorizationContextBuilder;

        public AuthorizationCookieAuthEventSubscriber(IPermissionSetProvider permissionSetProvider, IAuthorizationContextBuilder authorizationContextBuilder)
        {
            this.permissionSetProvider = permissionSetProvider;
            this.authorizationContextBuilder = authorizationContextBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks>
        /// This is a very important part of the demonstration. Here is where we're getting a call from within the 
        /// CookieAuthenticationOptions.Events.OnSigningIn so that we can clear the old permissions out and reset them for the 
        /// security context
        /// </remarks>
        public Task SigningIn(CookieSigningInContext context)
        {
            var authorizationContext = authorizationContextBuilder.Build(context.Principal);
            permissionSetProvider.Clear(authorizationContext.Key);

            return Task.CompletedTask;
        }
    }
}
