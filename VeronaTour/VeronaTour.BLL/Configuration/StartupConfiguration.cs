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
    public class StartupConfiguration
    {
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

            System.Data.Entity.Database.SetInitializer(new VeronaTourDbInitializer());
            var context = new VeronaTourDbContext();
            context.Database.Initialize(true);


            CreateRoles();
        }

        private static void CreateRoles()
        {
            var context = new VeronaTourDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<User>(new UserStore<User>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool    
                var roleAdmin = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                roleAdmin.Name = "Admin";
                roleManager.Create(roleAdmin);

                //Here we create a Admin super user who will maintain the website                   

                var user = new User();
                user.UserName = "vkorzhevyh@gmail.com";
                user.Name = "Veronika Korzhevykh";
                user.Email = "vkorzhevyh@gmail.com";
                user.PhoneNumber = "+19706792065";
                user.Password = "Qwerty6_";

                var chkUser = UserManager.Create(user, user.Password);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }
 
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Client"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Client";
                roleManager.Create(role);
            }
        }
    }
}
