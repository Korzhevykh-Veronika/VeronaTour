using System.Collections.Generic;
using VeronaTour.BLL.DTOs;

namespace VeronaTour.WEB.Models
{
    public class UsersViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string IsBlocked { get; set; }
        public string SelectedRole { get; set; }
    }
}