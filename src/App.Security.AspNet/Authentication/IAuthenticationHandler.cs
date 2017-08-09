using App.Security.AspNet.Authentication.Auth0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Security.AspNet.Authentication
{
    /// <summary>
    /// Manages authentication
    /// </summary>
    public interface IAuthenticationHandler
    {
        LockContext CreateLockContext(string returnUrl = null);

        Task SignOutAsync();
    }
}
