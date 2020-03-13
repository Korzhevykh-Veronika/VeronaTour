using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeronaTour.DAL.Entites
{
    public class User : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [RegularExpression(@"^[A-Za-z'-'\s]+$", ErrorMessage = "Wrong name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "User name length should be between 2 and 50 symbols")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "User name")]
        public string Name { get; set; }

        //[RegularExpression(@"^\+\d{11}$", ErrorMessage = "Wrong telephone number")]
        //[Required(ErrorMessage = "Field must be set")]
        //[Display(Name = "Phone")]
        //public string Phone { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Email")]
        public override string Email { get; set; }

        [Display(Name = "Password")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required(ErrorMessage = "Field must be set")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Range(0, 100, ErrorMessage = "Value should be between 0 and 100")]
        [Display(Name = "Sale")]
        public int Sale { get; set; }
        //public IEnumerable<string> Roles { get; set; }

        public bool IsBlocked { get; set; } = false;

        public virtual UserRole Role { get; set; }
    }
}
