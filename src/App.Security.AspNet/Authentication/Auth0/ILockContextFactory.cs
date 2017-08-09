

namespace App.Security.AspNet.Authentication.Auth0
{
    public interface ILockContextFactory
    {
        /// <summary>
        /// Creates lock context's with an optional return url
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        LockContext Create(string returnUrl = null);
    }
}
