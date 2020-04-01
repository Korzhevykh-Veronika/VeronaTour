using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using VeronaTour.DAL.EF;
using VeronaTour.DAL.Entites;

namespace VeronaTour.BLL.Utils
{
    /// <summary>
    /// Configure the application user manager used in this application. 
    /// UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        /// <summary>
        ///     Creates an instance of application user manager with predefined validation logic
        /// </summary>
        /// <param name="options">Identity factory options</param>
        /// <param name="context">Owin context</param>
        /// <returns>Application user manager</returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<User>(new VeronaTourDbContext()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    /// <summary>
    ///     Configure the application sign-in manager which is used in this application.
    /// </summary>
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(
            ApplicationUserManager userManager, 
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        /// <summary>
        ///     Creates a ClaimsIdentity representing the user
        /// </summary>
        /// <param name="user">User for getting own identity</param>
        /// <returns>User identity</returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return ((ApplicationUserManager)UserManager).CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        /// <summary>
        ///     Sign in the user in using the user name and password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <param name="rememberMe">Should application ask for login next time</param>
        /// <param name="shouldLockout">Should application lock theuser</param>
        /// <returns></returns>
        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout)
        {
            var user = UserManager.FindByEmailAsync(userName).Result;

            if (user != null && user.IsBlocked)
                return Task.FromResult<SignInStatus>(SignInStatus.LockedOut);

            return base.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
        }

        /// <summary>
        ///     Creates an instance of application sign in manager
        /// </summary>
        /// <param name="options">Identity factory options</param>
        /// <param name="context">Owin context</param>
        /// <returns>Application sign in manager</returns>
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
