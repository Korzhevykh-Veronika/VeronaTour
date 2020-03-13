using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.DAL.Entites
{
    public class SaleSettings
    {
        public int Id { get; set; }
        public int SaleStep { get; set; }
        public int MaxSale { get; set; }
    }
}
