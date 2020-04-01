using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Utils;

namespace VeronaTour.BLL.Services.Interfaces
{
    public interface IIdentityService
    {
        Task UpdateUser(
            string userEmail,
            UserDTO userDto,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager);

        UserDTO GetUserByEmail(
            string email,
            ApplicationUserManager userManager);

        Task<IEnumerable<UserDTO>> GetUsers(ApplicationUserManager userManager);
        
        Task<IEnumerable<string>> GetRoles();

        Task UpdateUserSettings(
            string email,
            string selectedRole,
            string isBlocked,
            ApplicationUserManager userManager);

        Task<IEnumerable<string>> RegisterUser(UserDTO user,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager);

        Task<SignInStatus> SignInAsync(
          ApplicationSignInManager signInManager,
          string username,
          string password,
          bool rememberMe,
          bool shouldLockout = false);

        Task RecalculateSaleByOrder(int orderId, ApplicationUserManager userManager, ApplicationSignInManager signInManager);
    }
}
