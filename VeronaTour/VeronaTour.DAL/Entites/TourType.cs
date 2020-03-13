using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.DAL.Entites
{
    public class TourType
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}

