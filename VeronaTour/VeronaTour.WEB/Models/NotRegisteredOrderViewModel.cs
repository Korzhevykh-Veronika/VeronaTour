using VeronaTour.BLL.DTOs;

namespace VeronaTour.WEB.Models
{
    public class NotRegisteredOrderViewModel
    {
        public int OrderId { get; set; }
        public TourDTO Tour { get; set; }
        public int NumberOfPeople { get; set; }
    }
}