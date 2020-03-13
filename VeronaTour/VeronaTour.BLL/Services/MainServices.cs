using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VeronaTour.BLL.DTOs;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;

namespace VeronaTour.BLL.Services
{
    public class MainService : IMainService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;

        const string variantAll = "Choose...";

        public MainService(IUnitOfWork newUnitOfWork, IMapper newMapper)
        {
            unitOfWork = newUnitOfWork;
            mapper = newMapper;
        }

        public IEnumerable<TourTypeDTO> GetTourTypes()
        {
            return mapper.Map<IEnumerable<TourTypeDTO>>(unitOfWork.TourTypes.GetAll());
        }

        public HotelDTO GetHotel(int id)
        {
            return mapper.Map<HotelDTO>(unitOfWork.Hotels.Get(id));
        }        

        public int GetMaxCountOfStarsForHotel()
        {
            return 5; // constant
        }
        public string GetTommorowDate()
        {
            var date = DateTime.Now.AddDays(1);
            return date.ToString("MM/dd/yyyy");
        }

        public string GetInMonthDate()
        {
            var date = DateTime.Now.AddDays(31);
            return date.ToString("MM/dd/yyyy");
        }

        public string GetIn2WeeksDate()
        {
            var date = DateTime.Now.AddDays(14);
            return date.ToString("MM/dd/yyyy");
        }
        public IEnumerable<FeedingTypeDTO> GetFeedingTypes()
        {
            return mapper.Map<IEnumerable<FeedingTypeDTO>>(unitOfWork.FeedingTypes.GetAll());
        }

        
        public IEnumerable<HotelDTO> GetHotels()
        {
            return mapper.Map<IEnumerable<HotelDTO>>(unitOfWork.Hotels.GetAll().OrderBy(h => h.IsDeleted).ThenByDescending(h => h.Id));
        }
        public IEnumerable<UserDTO> GetUsers()
        {
            return mapper.Map<IEnumerable<UserDTO>>(unitOfWork.Users.GetAll());
        }
        public IEnumerable<string> GetTourTypesTitles()
        {
            var allTourTypes = unitOfWork.TourTypes.GetAll().Select(tourtype => tourtype.Title).ToList();

            allTourTypes.Insert(0, variantAll);

            return allTourTypes;
        }

        public IEnumerable<string> GetFeedingTypesTitles()
        {
            var allFeedingTypes = unitOfWork.FeedingTypes.GetAll().Select(feedingType => feedingType.Title).ToList();

            allFeedingTypes.Insert(0, variantAll);

            return allFeedingTypes;
        }

        public IEnumerable<string> GetHotelsTitles()
        {
            var allHotelsTitles = unitOfWork.Hotels.GetAll().Select(hotel => hotel.Title + " * " + hotel.StarsCount).ToList();

            allHotelsTitles.Insert(0, variantAll);

            return allHotelsTitles;
        }

        public IEnumerable<string> GetCountries()
        {
            var allCountries = unitOfWork.Countries.GetAll().Select(country => country.Title).ToList();

            allCountries.Insert(0, variantAll);

            return allCountries;
        }

        




        public IEnumerable<string> AddCountry(CountryDTO country)
        {
            var errors = new List<string>();

            var countryEntity = mapper.Map<Country>(country);

            if (unitOfWork.Countries.Find(c => c.Title == country.Title).Any())
            {
                errors.Add("This country has been already presented.");
            }


            if (errors.Count() == 0)
            {
                unitOfWork.Countries.Create(countryEntity);
            }

            return errors;
        }



        public IEnumerable<string> AddTourType(TourTypeDTO tourType)
        {
            var errors = new List<string>();

            var tourtypeEntity = mapper.Map<TourType>(tourType);

            if (unitOfWork.TourTypes.Find(c => c.Title == tourType.Title).Any())
            {
                errors.Add("This tour type has been already presented.");
            }

            if (errors.Count() == 0)
            {
                unitOfWork.TourTypes.Create(tourtypeEntity);
            }

            return errors;
        }

        
        public IEnumerable<string> AddHotel(HotelDTO hotel)
        {
            var errors = new List<string>();

            var hotelEntity = mapper.Map<Hotel>(hotel);

            if (unitOfWork.Hotels.Find(h => h.Title == hotel.Title).Any())
            {
                errors.Add("This hotel has been already presented.");
            }

            if (errors.Count() == 0)
            {
                unitOfWork.Hotels.Create(hotelEntity);
            }

            return errors;
        }
        

        public IEnumerable<string> EditHotel(HotelDTO hotel)
        {
            var errors = new List<string>();

            var hotelEntity = unitOfWork.Hotels.Get(hotel.Id);

            if (errors.Count() == 0)
            {
                hotelEntity.Id = hotel.Id;
                hotelEntity.Title = hotel.Title;
                hotelEntity.StarsCount = hotel.StarsCount;
                hotelEntity.Description = hotel.Description;

                unitOfWork.Hotels.Update(hotelEntity);
            }

            return errors;
        }

       

        public IEnumerable<string> DeleteHotel(int id)
        {
            var result = new List<string>();

            var hotel = unitOfWork.Hotels.Get(id);

            if (unitOfWork.Tours.Find(t => t.Hotel.Id == id).Any())
            {
                result.Add("The hotel cannot be deleted, because some tours reference to it");
            }

            if (result.Count() == 0 && hotel != null)
            {
                hotel.IsDeleted = true;
                unitOfWork.Hotels.Update(hotel);
            }

            return result;
        }

        
    }
}
