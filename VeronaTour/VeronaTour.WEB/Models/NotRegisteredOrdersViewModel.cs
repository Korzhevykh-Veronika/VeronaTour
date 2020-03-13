using System.Collections.Generic;

namespace VeronaTour.WEB.Models
{
    public class NotRegisteredOrdersViewModel
    {
        public IEnumerable<NotRegisteredOrderViewModel> NotRegisteredOrder { get; set; }
        public double Sale { get; set; }
        public int TotalPrice { get; set; }
        
    }
}