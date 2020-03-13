using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeronaTour.BLL.DTOs
{
    public class TourDTO
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Wrong tour title")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Title length should be between 2 and 20 symbols")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Title of tour")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }


        //[DataType(DataType.Currency)]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Price for one")]
        public int Price { get; set; }

        [Range(1, 50, ErrorMessage = "Value should be between 1 and 10")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Minimal count of people")]
        public int MinPeopleCount { get; set; }

        [Range(1, 50, ErrorMessage = "Value should be between 1 and 10")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Maximal count of people")]
        public int MaxPeopleCount { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        [Display(Name = "Hot Tour")]
        public bool HotTour { get; set; }
        public bool IsDeleted { get; set; } = false;

        [Range(1, 100, ErrorMessage = "Value should be between 1 and 100")]
        [Required(ErrorMessage = "Field must be set")]
        [Display(Name = "Count of tours")]
        public int CountOfTour { get; set; }

        public virtual TourTypeDTO Type { get; set; }
        public virtual FeedingTypeDTO FeedingType { get; set; }
        public virtual HotelDTO Hotel { get; set; }
        public virtual CountryDTO Country { get; set; }
    }
}
