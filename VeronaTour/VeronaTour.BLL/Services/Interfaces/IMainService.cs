using System.Collections.Generic;
using System.Web;
using VeronaTour.BLL.DTOs;
using VeronaTour.DAL.Entites;

namespace VeronaTour.BLL.Services
{
    public interface IMainService
    {
        HotelDTO GetHotel(int id);
        int GetMaxCountOfStarsForHotel();
        IEnumerable<FeedingTypeDTO> GetFeedingTypes();
        IEnumerable<TourTypeDTO> GetTourTypes();
        IEnumerable<HotelDTO> GetHotels();
        IEnumerable<UserDTO> GetUsers();
        IEnumerable<string> GetTourTypesTitles();
        IEnumerable<string> GetFeedingTypesTitles();
        IEnumerable<string> GetHotelsTitles();
        IEnumerable<string> GetCountries();
        string GetTommorowDate();
        string GetInMonthDate();
        string GetIn2WeeksDate();

        IEnumerable<string> DeleteHotel(int id);
        IEnumerable<string> AddCountry(CountryDTO country);
        IEnumerable<string> AddTourType(TourTypeDTO tourType);
        IEnumerable<string> AddHotel(HotelDTO hotel);        
        IEnumerable<string> EditHotel(HotelDTO hotel);
    }
}
