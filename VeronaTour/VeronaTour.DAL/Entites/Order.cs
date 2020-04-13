using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeronaTour.DAL.Entites
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
