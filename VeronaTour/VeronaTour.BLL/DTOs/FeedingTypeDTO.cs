using System.ComponentModel.DataAnnotations;

namespace VeronaTour.BLL.DTOs
{
    public class FeedingTypeDTO
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [RegularExpression(@"^(?!Choose...)[A-Za-z'-'\s]+$", ErrorMessage = "Wrong type of food")]
        [Display(Name = "Type of food")]
        [Required(ErrorMessage = "Field must be set")]
        public string Title { get; set; }
    }
}

