using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Security.AspNet.Authentication.Auth0
{
    public class Auth0Settings
    {
        public const string ConfigurationSectionName = "Auth0";

        public static class AuthenticationScheme
        {
            public const string OpenIdConnect = "Auth0";
            public const string Cookie = CookieAuthenticationDefaults.AuthenticationScheme;
        }

        /// <summary>
        /// Gets the domain managed in Auth0. Used as the OIDC authority.
        /// </summary>
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0.
        /// Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
        /// </summary>
        public string CallbackPath { get; set; }

        public string ClaimsIssuer { get; set; }
    }
}
