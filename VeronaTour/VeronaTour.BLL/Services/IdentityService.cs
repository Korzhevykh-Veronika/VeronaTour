using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services.Interfaces;
using VeronaTour.BLL.Utils;
using VeronaTour.DAL.EF;
using VeronaTour.DAL.Entites;

namespace VeronaTour.BLL.Services
{
    public class IdentityService : IIdentityService
    {
        //private ApplicationUserManager applicationUserManager;
        //private ApplicationSignInManager applicationSignInManager;

        //public UserStore<User> userStoreManager;

        private IMapper mapper;
        private IOrdersService ordersService;

        public IdentityService(
             IMapper newMapper,
             IOrdersService newOrdersService)
        //ApplicationUserManager applicationUserManager,
        //ApplicationSignInManager applicationSignInManager)
        {
            ordersService = newOrdersService;
            mapper = newMapper;
        }

        protected RoleManager<IdentityRole> RoleManager
        {
            get
            {
                var context = new VeronaTourDbContext();
                return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            }
        }

        public async Task UpdateUser(
            string userEmail,
            UserDTO userDto,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            var user = userManager.FindByNameAsync(userEmail).Result;

            user.Email = userDto.Email;
            user.UserName = userDto.Email;
            user.Password = userDto.Password;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Name = userDto.Name;

            await userManager.UpdateAsync(user);

            var userStoreManager = new UserStore<User>(new VeronaTourDbContext());

            await userManager.UpdateSecurityStampAsync(user.Id).ConfigureAwait(false);
            await signInManager.SignInAsync(user, true, false).ConfigureAwait(false);

            userStoreManager.Context.SaveChanges();
        }

        public UserDTO GetUserByEmail(
            string email,
            ApplicationUserManager userManager)
        {
            return mapper.Map<UserDTO>(userManager.FindByEmailAsync(email).Result);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(ApplicationUserManager userManager)
        {
            var userEntities = await userManager.Users.ToListAsync();
            var usersDTO = new List<UserDTO>();

            foreach (var user in userEntities)
            {
                var userDto = mapper.Map<UserDTO>(user);

                userDto.Role = (await userManager.GetRolesAsync(user.Id)).FirstOrDefault();

                usersDTO.Add(userDto);
            }

            return usersDTO;
        }

        public async Task RecalculateSaleByOrder(
            int orderId,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            var userEmail = ordersService.GetOrder(orderId).User.Email;
            var user = await userManager.FindByNameAsync(userEmail);

            ordersService.RecalculateSale(user);

            await userManager.UpdateAsync(user);

            var userStoreManager = new UserStore<User>(new VeronaTourDbContext());
            await userManager.UpdateSecurityStampAsync(user.Id).ConfigureAwait(false);
            await signInManager.SignInAsync(user, true, false).ConfigureAwait(false);

            userStoreManager.Context.SaveChanges();
        }

        public async Task UpdateUserSettings(
            string email,
            string selectedRole,
            string isBlocked,
            ApplicationUserManager userManager)
        {
            var user = await userManager.FindByNameAsync(email);

            if (user != null)
            {
                var previousRoles = userManager.GetRoles(user.Id);
                userManager.RemoveFromRoles(user.Id, previousRoles.ToArray());

                await userManager.AddToRoleAsync(user.Id, selectedRole);

                user.IsBlocked = isBlocked == "on";

                await userManager.UpdateAsync(user);
                await userManager.UpdateSecurityStampAsync(user.Id).ConfigureAwait(false);

                var userStoreManager = new UserStore<User>(new VeronaTourDbContext());
                userStoreManager.Context.SaveChanges();
            }
        }

        public async Task<IEnumerable<string>> GetRoles()
        {
            var context = new VeronaTourDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            return await roleManager.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task<IEnumerable<string>> RegisterUser(UserDTO user,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            var userToAdd = new User()
            {
                Name = user.Name,
                UserName = user.Email,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password
            };

            var result = await userManager.CreateAsync(userToAdd, user.Password);
            if (result.Succeeded)
            {
                var newUser = userManager.FindByEmail(userToAdd.Email);
                await userManager.AddToRoleAsync(newUser.Id, "Client");
                await signInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);
            }

            return result.Errors;
        }

        public async Task<SignInStatus> SignInAsync(
            ApplicationSignInManager signInManager,
            string username,
            string password,
            bool rememberMe,
            bool shouldLockout = false)
        {
            return await signInManager.PasswordSignInAsync(
                username,
                password,
                rememberMe, 
                shouldLockout);
        }
    }
}
