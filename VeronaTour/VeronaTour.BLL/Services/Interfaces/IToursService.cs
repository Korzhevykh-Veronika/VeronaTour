using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VeronaTour.BLL.DTOs;

namespace VeronaTour.BLL.Services.Interfaces
{
    public interface IToursService
    {
        void ChangeTourHot(int id);

        IEnumerable<string> EditTour(TourDTO tour,
            string selectedHotel,
            string selectedFeedingType,
            string selectedTourType,
            string selectedCountry,
            HttpPostedFileBase uploadImage);

        IEnumerable<string> AddTour(
           TourDTO tour,
           string selectedHotel,
           string selectedFeedingType,
           string selectedTourType,
           string selectedCountry,
           HttpPostedFileBase uploadImage);

        IEnumerable<TourDTO> GetFilteredTours(FilterDTO filterOptions); // t
        IEnumerable<TourDTO> GetTours(); // t
        IEnumerable<TourDTO> GetSortAdminTours(); // t
        IEnumerable<TourDTO> GetSortTours(); // t
        TourDTO GetTour(int id); // t
        IEnumerable<TourDTO> GetHotTours(); // t
        void DeleteTour(int id);

    }
}
