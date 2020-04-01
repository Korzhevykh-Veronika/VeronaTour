using System.Collections.Generic;
using VeronaTour.BLL.DTOs;

namespace VeronaTour.WEB.Models
{
    public class OrdersViewModel
    {
        public IEnumerable<OrderDTO> RegisteredOrder { get; set; }
        public double Sale { get; set; }
        public int TotalPrice { get; set; }
    }
}