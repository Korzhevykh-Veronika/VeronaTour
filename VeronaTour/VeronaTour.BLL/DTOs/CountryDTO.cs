using System.ComponentModel.DataAnnotations;

namespace VeronaTour.BLL.DTOs
{
    public class CountryDTO
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [RegularExpression(@"^(?!Choose...)[A-Za-z\s]+$", ErrorMessage = "Wrong country")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Country length should be between 2 and 20 symbols")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Country")]
        public string Title { get; set; }
    }
}
