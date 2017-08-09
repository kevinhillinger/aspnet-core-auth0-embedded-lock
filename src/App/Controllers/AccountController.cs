using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using App.Security.AspNet.Authentication;
using System.Threading.Tasks;

namespace SampleMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationHandler authenticationHandler;

        public AccountController(IAuthenticationHandler authenticationHandler)
        {
            this.authenticationHandler = authenticationHandler;
        }

        public IActionResult Login(string returnUrl = "/")
        {
            var lockContext = authenticationHandler.CreateLockContext(returnUrl);
            return View(lockContext);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authenticationHandler.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            return View(User.Claims);
        }
    }
}
