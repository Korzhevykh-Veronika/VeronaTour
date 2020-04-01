using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeronaTour.DAL.Entites
{
    public class User : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Name { get; set; }
        public override string Email { get; set; }
        public string Password { get; set; }
        public int Sale { get; set; }
        public bool IsBlocked { get; set; } = false;
    }
}
