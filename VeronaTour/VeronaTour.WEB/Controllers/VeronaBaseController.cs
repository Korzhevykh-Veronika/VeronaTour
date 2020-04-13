using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services;
using VeronaTour.BLL.Services.Interfaces;
using VeronaTour.BLL.Utils;

namespace VeronaTour.WEB.Controllers
{
    [Authorize]
    public abstract class VeronaBaseController : Controller
    {
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        protected IMainService mainService;
        protected IIdentityService identityService;
        protected ILogger logger;

        protected UserDTO currentUser;

        protected VeronaBaseController(IMainService newMainService, IIdentityService newIdentityService, ILogger newLogger)
        {
            mainService = newMainService;
            identityService = newIdentityService;
            logger = newLogger;

            currentUser = GetCurrentUser();

            if (currentUser != null && currentUser.IsBlocked)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
        }

        protected void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                if(signInManager == null)
                {
                    signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                }

                return signInManager;
            }
            private set
            {
                signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                {
                     userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }

                return userManager;

            }
            private set
            {
                userManager = value;
            }
        }

        private new HttpContextBase HttpContext
        {
            get
            {
                var context = new HttpContextWrapper(System.Web.HttpContext.Current);

                return (HttpContextBase)context;
            }
        }

        private UserDTO GetCurrentUser()
        {
            var user = identityService.GetUserByEmail(
                System.Web.HttpContext.Current.User.Identity.Name,
                UserManager);

            return user;
        }
    }
}