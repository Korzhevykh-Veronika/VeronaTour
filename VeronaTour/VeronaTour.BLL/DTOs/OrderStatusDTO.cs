using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.BLL.DTOs
{
    public class OrderStatusDTO
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [RegularExpression(@"^[A-Za-z'-'\s]+$", ErrorMessage = "Wrong order status")]
        [Display(Name = "Order Status")]
        [Required(ErrorMessage = "Field must be set")]
        public string Title { get; set; }
    }
}
