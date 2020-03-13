using System.Collections.Generic;
using VeronaTour.BLL.DTOs;

namespace VeronaTour.WEB.Models
{
    public class ToursViewModel
    {
        public IEnumerable<TourDTO> Tours { get; set; }
        public IEnumerable<string> TourTypeTitles  { get; set; }
        public List<FeedingTypeDTO> FeedingTypes { get; set; }
        
        public int MaxCountOfStartForHotel { get; set; }
        public int MaxCountOfPeople { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public string StartDateTime { get; set; }
        public IEnumerable<string> Countries { get; set; }
        public PageInfo PageInfo { get; set; }
        public FilterDTO FilterOptions { get; set; }
    }
}