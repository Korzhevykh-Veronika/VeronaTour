using System.ComponentModel.DataAnnotations;

namespace VeronaTour.BLL.DTOs
{
    public class HotelDTO
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        [RegularExpression(@"^(?!Choose...)[A-Za-z'-'\s]+$", ErrorMessage = "Wrong hotel title")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Hotel title length should be between 2 and 40 symbols")]
        [Display(Name = "Hotel title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Hotel description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Range(1, 5, ErrorMessage = "Value should be between 1 and 5")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Hotel stars count")]
        public int StarsCount { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
