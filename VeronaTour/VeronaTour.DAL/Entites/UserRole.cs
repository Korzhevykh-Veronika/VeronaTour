using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace VeronaTour.DAL.Entites
{
    public class UserRole : IdentityRole
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [RegularExpression(@"^[A-Za-z'-'\s]+$", ErrorMessage = "Wrong user role")]
        [Display(Name = "User Role")]
        [Required(ErrorMessage = "Field must be set")]
        public string Name { get; set; }
    }
}
