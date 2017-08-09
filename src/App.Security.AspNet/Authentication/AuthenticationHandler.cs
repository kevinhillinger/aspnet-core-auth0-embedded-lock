using App.Security.AspNet.Authentication.Auth0;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System.Threading.Tasks;

namespace App.Security.AspNet.Authentication
{
    class AuthenticationHandler : IAuthenticationHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILockContextFactory lockContextFactory;

        public AuthenticationHandler(IHttpContextAccessor httpContextAccessor, ILockContextFactory lockContextFactory)
        {
            this.lockContextFactory = lockContextFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        public LockContext CreateLockContext(string returnUrl = null)
        {
            return lockContextFactory.Create(returnUrl);
        }

        public Task SignOutAsync()
        {
            var authenticationManager = GetAuthenticationManager();

            //call both to clear out all auth schemes.

            authenticationManager.SignOutAsync(Auth0Settings.AuthenticationScheme.OpenIdConnect).ConfigureAwait(false);
            authenticationManager.SignOutAsync(Auth0Settings.AuthenticationScheme.Cookie).ConfigureAwait(false);

            return Task.CompletedTask;
        }

        private AuthenticationManager GetAuthenticationManager()
        {
            return httpContextAccessor.HttpContext.Authentication; //violation of law of demeter, but aspnet core sucks in this way
        }
    }
}
