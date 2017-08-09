using App.Security.AspNet.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace App.Security.AspNet.Auth0
{
    public static class Auth0Extensions
    {
        public static void UseAuth0(this IApplicationBuilder app, OpenIdConnectOptions options, AuthorizationCookieAuthEventSubscriber subscriber)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Events = new CookieAuthenticationEvents
                {
                    OnSigningIn = subscriber.SigningIn
                }
            });
            
            app.UseOpenIdConnectAuthentication(options);
        }
    }
}