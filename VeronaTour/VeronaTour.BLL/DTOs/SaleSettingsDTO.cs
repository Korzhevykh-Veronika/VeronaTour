using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.BLL.DTOs
{
    public class SaleSettingsDTO
    {
        [ScaffoldColumn(false)]

        public int Id { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Wrong sale step")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Sale step")]
        public int SaleStep { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Wrong maximum sale")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Maximum sale")]
        public int MaxSale { get; set; }
    }
}
