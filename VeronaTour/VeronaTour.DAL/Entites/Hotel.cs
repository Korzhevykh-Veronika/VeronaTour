using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.DAL.Entites
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }
        public int StarsCount { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
