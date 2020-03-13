using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.BLL.DTOs
{
    public class FilterDTO
    {
        public string SelectedCountry { get; set; }
        public string SelectedTourType { get; set; }
        public string SelectedStartDate { get; set; }
        public string SelectedEndDate { get; set; }
        public int SelectedNumberOfPeople { get; set; }
        public int SelectedPrice { get; set; }
        public int SelectedPage { get; set; }
        public List<bool> CheckedFood { get; set; } = new List<bool>();
        public List<bool> CheckedHotel { get; set; } = new List<bool>();
    }
}
