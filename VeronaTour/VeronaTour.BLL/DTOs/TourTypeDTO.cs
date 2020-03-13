using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.BLL.DTOs
{
    public class TourTypeDTO
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [RegularExpression(@"^(?!Choose...)[A-Za-z'-'\s]+$", ErrorMessage = "Wrong tour type")]
        [Display(Name = "Tour type")]
        [Required(ErrorMessage = "Field must be set")]
        public string Title { get; set; }
    }
}
