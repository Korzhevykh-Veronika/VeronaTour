using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using VeronaTour.BLL.Utils;
using VeronaTour.DAL.EF;
using VeronaTour.DAL.Entites;

namespace VeronaTour.BLL.Configuration
{
    /// <summary>
    ///     Configures application on startup
    /// </summary>
    public class StartupConfiguration
    {
        /// <summary>
        ///     Configures identity options and settings, creates nessesary entities
        /// </summary>
        /// <param name="app">Stores application startup settings</param>
        public static void ConfigureIdentity(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new VeronaTourDbContext());
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            CreateRoles();
        }

        /// <summary>
        ///     Creates Admin (plus default user), Manager and Client
        ///     identity roles and saves them to the DB.
        /// </summary>
        private static void CreateRoles()
        {
            var context = new VeronaTourDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<User>(new UserStore<User>(context));
  
            if (!roleManager.RoleExists("Admin"))
            {
                var roleAdmin = new IdentityRole();
                roleAdmin.Name = "Admin";
                roleManager.Create(roleAdmin);

                var user = new User();
                user.UserName = "vkorzhevyh@gmail.com";
                user.Name = "Veronika Korzhevykh";
                user.Email = "vkorzhevyh@gmail.com";
                user.PhoneNumber = "+19706792065";
                user.Password = "Qwerty6_";

                var chkUser = UserManager.Create(user, user.Password);
   
                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }
 
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Client"))
            {
                var role = new IdentityRole();
                role.Name = "Client";
                roleManager.Create(role);
            }
        }
    }
}
