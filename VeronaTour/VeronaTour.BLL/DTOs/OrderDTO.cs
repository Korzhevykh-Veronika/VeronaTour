using System;
using System.ComponentModel.DataAnnotations;

namespace VeronaTour.BLL.DTOs
{
    public class OrderDTO
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public TourDTO Tour  { get; set; }
        public OrderStatusDTO Status { get; set; }

        [Display(Name = "Number of people")]
        [Required(ErrorMessage = "Field must be set")]
        public int NumberOfPeople { get; set; }
        public int FinalSale{ get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateUpdateOrder { get; set; }
    }
}
