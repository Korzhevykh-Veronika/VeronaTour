using System.Collections.Generic;

namespace VeronaTour.BLL.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
               
        public string Email { get; set; }
                
        public string Password { get; set; }
                
        public string Name { get; set; }
                
        public string PhoneNumber { get; set; }

        //public OrderStatusDTO Status { get; set; }
     
        public double Sale { get; set; }

        //public  UserRoleDTO Role { get; set; }

        public string Role { get; set; }

        public bool IsBlocked { get; set; }
    }
}
