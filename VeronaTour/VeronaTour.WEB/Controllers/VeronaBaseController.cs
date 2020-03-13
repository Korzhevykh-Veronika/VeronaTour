using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services;
using VeronaTour.BLL.Services.Interfaces;
using VeronaTour.BLL.Utils;

namespace VeronaTour.WEB.Controllers
{
    [Authorize]
    public class VeronaBaseController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        protected IMainService mainService;
        protected IIdentityService identityService;

        protected UserDTO currentUser;

        protected VeronaBaseController(IMainService newMainService, IIdentityService newIdentityService)
        {
            mainService = newMainService;
            identityService = newIdentityService;

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

        protected double GetSale()
        {
            return 1.0 - (double)currentUser.Sale / 100;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected new HttpContextBase HttpContext
        {
            get
            {
                var context = new HttpContextWrapper(System.Web.HttpContext.Current);

                return (HttpContextBase)context;
            }
        }

        private UserDTO GetCurrentUser()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = identityService.GetUserByEmail(
                System.Web.HttpContext.Current.User.Identity.Name,
                userManager);

            return user;
        } 
    }
}