using System.Collections.Generic;
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

        IEnumerable<TourDTO> GetFilteredTours(FilterDTO filterOptions); 
        IEnumerable<TourDTO> GetTours();
        IEnumerable<TourDTO> GetSortAdminTours(); 
        IEnumerable<TourDTO> GetSortTours(); 
        TourDTO GetTour(int id, bool showAll);
        IEnumerable<TourDTO> GetHotTours(); 
        void DeleteTour(int id);

    }
}
