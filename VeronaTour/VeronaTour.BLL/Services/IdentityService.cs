using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services.Interfaces;
using VeronaTour.BLL.Utils;
using VeronaTour.DAL.EF;
using VeronaTour.DAL.Entites;

namespace VeronaTour.BLL.Services
{
    /// <summary>
    ///     Contains business logic connected with identities
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private IMapper mapper;
        private IOrdersService ordersService;
        private ILogger logger;

        public IdentityService(IMapper newMapper,IOrdersService newOrdersService, ILogger newLogger)
        {
            ordersService = newOrdersService;
            mapper = newMapper;
            logger = newLogger;
        }

        /// <summary>
        ///     Updates user information 
        /// </summary>
        /// <param name="userEmail">Unique identifier for search user entity</param>
        /// <param name="userDto">Contains new data for updating</param>
        /// <param name="userManager">Manager of users</param>
        /// <param name="signInManager">Sign in manager</param>
        /// <returns></returns>
        public async Task UpdateUser(
            string userEmail,
            UserDTO userDto,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            var user = await userManager.FindByNameAsync(userEmail);

            user.Email = userDto.Email;
            user.UserName = userDto.Email;
            user.Password = userDto.Password;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Name = userDto.Name;

            await userManager.UpdateAsync(user);

            var userStoreManager = new UserStore<User>(new VeronaTourDbContext());

            await userManager.UpdateSecurityStampAsync(user.Id);
            await signInManager.SignInAsync(user, true, false);

            var token = await userManager.GeneratePasswordResetTokenAsync(user.Id);
            await userManager.ResetPasswordAsync(user.Id, token, user.Password);

            userStoreManager.Context.SaveChanges();

            logger.Info($"User {user.UserName} has been successfuly updated.");
        }

        /// <summary>
        ///     Get user by email
        /// </summary>
        /// <param name="email">Unique identifier for search user entity</param>
        /// <param name="userManager">Manager of users</param>
        /// <returns> User information </returns>
        public UserDTO GetUserByEmail(
            string email,
            ApplicationUserManager userManager)
        {
            return mapper.Map<UserDTO>(userManager.FindByEmailAsync(email).Result);
        }

        /// <summary>
        ///     Get all users
        /// </summary>
        /// <param name="userManager">Manager of users</param>
        /// <returns> Users information </returns>
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

        /// <summary>
        ///     Recalculate sale for user based on previous and new orders
        /// </summary>
        /// <param name="orderId">Order identifier</param>
        /// <param name="userManager">Manager of users</param>
        /// <param name="signInManager">Sign in manager</param>
        /// <returns></returns>
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

            logger.Info($"{user.UserName}`s sale has been successfuly recalculated.");
        }

        /// <summary>
        ///     Updates user settings
        /// </summary>
        /// <param name="email">Unique identifier for search a user entity</param>
        /// <param name="selectedRole">New user role</param>
        /// <param name="isBlocked">Should user be bloced by a system</param>
        /// <param name="userManager">Manager of users</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> UpdateUserSettings(
            string email,
            string selectedRole,
            string isBlocked,
            ApplicationUserManager userManager,
            string currentUserEmail)
        {
            var errors = new List<string>();

            if (email == currentUserEmail)
            {
                errors.Add("You cannot update yourself.");
                logger.Warn($"{email} tries to update himself.");
            }

            if (!errors.Any())
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

                    logger.Info($"{user.UserName}`s settings were updated.");
                }
                else
                {
                    logger.Warn($"Cannot find user by name {email}.");
                }
            }

            return errors;
        }

        /// <summary>
        ///     Get all roles from the DB
        /// </summary>
        /// <returns>Sequence of roles titles</returns>
        public async Task<IEnumerable<string>> GetRoles()
        {
            var context = new VeronaTourDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            return await roleManager.Roles.Select(r => r.Name).ToListAsync();
        }

        /// <summary>
        ///     Registers a user in the application (saves user data into DB)
        /// </summary>
        /// <param name="user">User`s information</param>
        /// <param name="userManager">Manager of users</param>
        /// <param name="signInManager">Sign in manager</param>
        /// <returns>Sequence of errors</returns>
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

                logger.Info($"New user {newUser.UserName} has been registered.");
            }

            logger.Warn($"Cannot register user {userToAdd.UserName}: " + String.Join(" ", result.Errors));

            return result.Errors;
        }

        /// <summary>
        ///     Sign in into the app and start a session
        /// </summary>
        /// <param name="signInManager">Sign in manager</param>
        /// <param name="username">Unique identifier for search user entity and login</param>
        /// <param name="password">Inputed password</param>
        /// <param name="rememberMe">Should app store the cookies for skipping login next time</param>
        /// <param name="shouldLockout">Shoud app block user</param>
        /// <returns> Status of sign in operation </returns>
        public async Task<SignInStatus> SignInAsync(
            ApplicationSignInManager signInManager,
            string username,
            string password,
            bool rememberMe,
            bool shouldLockout = false)
        {
            var status = await signInManager.PasswordSignInAsync(
                username,
                password,
                rememberMe, 
                shouldLockout);

            if(status == SignInStatus.Success)
            {
                logger.Info($"User {username} has been signed in.");
            }
            else
            {
                logger.Warn($"User {username} has not been signed in.");
            }

            return status;
        }
    }
}
