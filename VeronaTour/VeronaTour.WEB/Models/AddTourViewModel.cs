using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using VeronaTour.BLL.DTOs;

namespace VeronaTour.WEB.Models
{
    public class AddTourViewModel
    {   
        public TourDTO Tour { get; set; }
        public IEnumerable<string> TourTypeTitles { get; set; }
        public IEnumerable<string> FeedingTypes { get; set; }
        public IEnumerable<string> Countries { get; set; }
        public IEnumerable<string> HotelTitles { get; set; }

        [RegularExpression(@"^(?!Choose...)[A-Za-z'-'\s]+$", ErrorMessage = "Wrong tour type")]
        [Display(Name = "Tour type")]
        [Required(ErrorMessage = "Field must be set")]
        public string SelectedTourType { get; set; }

        [RegularExpression(@"^(?!Choose...)[A-Za-z'-'\s]+$", ErrorMessage = "Wrong type of food")]
        [Display(Name = "Type of food")]
        [Required(ErrorMessage = "Field must be set")]
        public string SelectedFeedingType { get; set; }

        [RegularExpression(@"^(?!Choose...)[A-Za-z\s]+$", ErrorMessage = "Wrong country")]
        //[StringLength(20, MinimumLength = 2, ErrorMessage = "Country length should be between 2 and 20 symbols")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Country")]
        public string SelectedCountry { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        [RegularExpression(@"^(?!Choose...)[A-Za-z'-'0-9'*'\s]+$", ErrorMessage = "Wrong hotel title")]
        //[StringLength(40, MinimumLength = 2, ErrorMessage = "Hotel title length should be between 2 and 40 symbols")]
        [Display(Name = "Hotel title")]
        public string SelectedHotelTitle { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        public HttpPostedFileBase uploadImage { get; set; }
    }
}