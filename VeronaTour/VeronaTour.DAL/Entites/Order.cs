using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.DAL.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Tour Tour { get; set; }
        public int NumberOfPeople { get; set; }
        public int FinalSale { get; set; }
        public virtual OrderStatus Status { get; set; }
        public DateTime? DateOrder { get; set; }
        public DateTime? DateUpdateOrder { get; set; }
    }
}
