using System.ComponentModel.DataAnnotations;

namespace VeronaTour.WEB.Models
{
    public class LoginViewModel
    {
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [RegularExpression(@"^[A-Za-z'-'\s]+$", ErrorMessage = "Wrong name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "User name length should be between 2 and 50 symbols")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "User name")]
        public string Name { get; set; }

        [RegularExpression(@"^\+\d{11}$", ErrorMessage = "Wrong telephone number")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
         
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }       

    }
    public class EditUserViewModel
    {
        [RegularExpression(@"^[A-Za-z'-'\s]+$", ErrorMessage = "Wrong name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "User name length should be between 2 and 50 symbols")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "User name")]
        public string Name { get; set; }

        [RegularExpression(@"^\+\d{11}$", ErrorMessage = "Wrong telephone number")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
