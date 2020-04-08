using AutoMapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using VeronaTour.BLL.DTOs;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;

namespace VeronaTour.BLL.Services
{
    /// <summary>
    ///     Contains business logic shared for all parts of an app
    /// </summary>
    public class MainService : IMainService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILogger logger;

        const string variantAll = "Choose...";

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="newUnitOfWork">DB access provider</param>
        /// <param name="newMapper">Mapper</param>
        /// <param name="newLogger">Logger</param>
        public MainService(IUnitOfWork newUnitOfWork, IMapper newMapper, ILogger newLogger)
        {
            unitOfWork = newUnitOfWork;
            mapper = newMapper;
            logger = newLogger;
        }

        /// <summary>
        ///     Get Tour types from the DB
        /// </summary>
        /// <returns>Tours information</returns>
        public IEnumerable<TourTypeDTO> GetTourTypes()
        {
            return mapper.Map<IEnumerable<TourTypeDTO>>(unitOfWork.TourTypes.GetAll());
        }

        /// <summary>
        ///     Get hotel information 
        /// </summary>
        /// <param name="id">Hotel identifier</param>
        /// <returns>Hotel information</returns>
        public HotelDTO GetHotel(int id)
        {
            return mapper.Map<HotelDTO>(unitOfWork.Hotels.Get(id));
        }        

        /// <summary>
        ///     Get maximum count of start for a hotel. Used for filtering
        /// </summary>
        /// <returns>Count of starts</returns>
        public int GetMaxCountOfStarsForHotel()
        {
            return 5; 
        }

        /// <summary>
        ///     Return tomorrow date. Used for filtering
        /// </summary>
        /// <returns>Tommorow date</returns>
        public string GetTommorowDate()
        {
            var date = DateTime.Now.AddDays(1);
            return date.ToString("MM/dd/yyyy");
        }

        /// <summary>
        ///     Return in month date. Used for filtering
        /// </summary>
        /// <returns>In month date</returns>
        public string GetInMonthDate()
        {
            var date = DateTime.Now.AddDays(31);
            return date.ToString("MM/dd/yyyy");
        }

        /// <summary>
        ///     Return in two weeks date. Used for filtering
        /// </summary>
        /// <returns>In two weeks date</returns>
        public string GetIn2WeeksDate()
        {
            var date = DateTime.Now.AddDays(14);
            return date.ToString("MM/dd/yyyy");
        }

        /// <summary>
        ///     Get all feeding types. Used for filtering
        /// </summary>
        /// <returns>All feeding types</returns>
        public IEnumerable<FeedingTypeDTO> GetFeedingTypes()
        {
            return mapper.Map<IEnumerable<FeedingTypeDTO>>(unitOfWork.FeedingTypes.GetAll());
        }
        
        /// <summary>
        ///     Get all hotels
        /// </summary>
        /// <returns>Hotels information</returns>
        public IEnumerable<HotelDTO> GetHotels()
        {
            return mapper.Map<IEnumerable<HotelDTO>>(unitOfWork.Hotels.GetAll().OrderBy(h => h.IsDeleted).ThenByDescending(h => h.Id));
        }

        /// <summary>
        ///     Get all logs from the DB
        /// </summary>
        /// <returns>Logs</returns>
        public IEnumerable<ExceptionDetailDTO> GetLogs()
        {
            return mapper.Map<IEnumerable<ExceptionDetailDTO>>(unitOfWork.Logs.GetAll().Reverse());
        }

        /// <summary>
        ///     Get users information
        /// </summary>
        /// <returns>Users information</returns>
        public IEnumerable<UserDTO> GetUsers()
        {
            return mapper.Map<IEnumerable<UserDTO>>(unitOfWork.Users.GetAll());
        }

        /// <summary>
        ///     Get all tour types titles. Used for filtering
        /// </summary>
        /// <returns>Tour type titles</returns>
        public IEnumerable<string> GetTourTypesTitles()
        {
            var allTourTypes = unitOfWork
                .TourTypes
                .GetAll()
                .Select(tourtype => tourtype.Title)
                .ToList();

            allTourTypes.Insert(0, variantAll);

            return allTourTypes;
        }

        /// <summary>
        ///     Get all feeding types titles. Used for filtering
        /// </summary>
        /// <returns>Feeding type titles</returns>
        public IEnumerable<string> GetFeedingTypesTitles()
        {
            var allFeedingTypes = unitOfWork
                .FeedingTypes
                .GetAll()
                .Select(feedingType => feedingType.Title)
                .ToList();

            allFeedingTypes.Insert(0, variantAll);

            return allFeedingTypes;
        }

        /// <summary>
        ///     Get all hotels titles. Used for filtering
        /// </summary>
        /// <returns>Hotels titles</returns>
        public IEnumerable<string> GetHotelsTitles()
        {
            var allHotelsTitles = unitOfWork
                .Hotels
                .Find(hotel => !hotel.IsDeleted)
                .Select(hotel => hotel.Title + " * " + hotel.StarsCount)
                .ToList();

            allHotelsTitles.Insert(0, variantAll);

            return allHotelsTitles;
        }

        /// <summary>
        ///     Get sequence of countries. Used for filtering
        /// </summary>
        /// <returns>Sequence of countries</returns>
        public IEnumerable<string> GetCountries()
        {
            var allCountries = unitOfWork.Countries.GetAll().Select(country => country.Title).ToList();

            allCountries.Insert(0, variantAll);

            return allCountries;
        }

        /// <summary>
        ///     Adds country to the DB
        /// </summary>
        /// <param name="country">Country informatino</param>
        /// <returns>Sequence of errors</returns>
        public IEnumerable<string> AddCountry(CountryDTO country)
        {
            var errors = new List<string>();

            var countryEntity = mapper.Map<Country>(country);

            if (unitOfWork.Countries.Find(c => c.Title == country.Title).Any())
            {
                errors.Add("This country has been already presented.");
                logger.Warn($"{country.Title} has been already presented ");
            }

            if (errors.Count() == 0)
            {
                unitOfWork.Countries.Create(countryEntity);
            }

            return errors;
        }

        /// <summary>
        ///     Adds tour type to the DB.
        /// </summary>
        /// <param name="tourType">Tour type information</param>
        /// <returns>Sequence of errors</returns>
        public IEnumerable<string> AddTourType(TourTypeDTO tourType)
        {
            var errors = new List<string>();

            var tourtypeEntity = mapper.Map<TourType>(tourType);

            if (unitOfWork.TourTypes.Find(c => c.Title == tourType.Title).Any())
            {
                errors.Add("This tour type has been already presented.");
                logger.Warn($"{tourType.Title} has been already presented ");
            }

            if (errors.Count() == 0)
            {
                unitOfWork.TourTypes.Create(tourtypeEntity);
            }

            return errors;
        }

        /// <summary>
        ///     Adds hotel into the DB.
        /// </summary>
        /// <param name="hotel">Hotel information</param>
        /// <returns>Sequence of errors</returns>
        public IEnumerable<string> AddHotel(HotelDTO hotel)
        {
            var errors = new List<string>();

            var hotelEntity = mapper.Map<Hotel>(hotel);

            hotel.Title = hotel.Title.Trim();

            if (unitOfWork.Hotels.Find(h => h.Title == hotel.Title).Any())
            {
                errors.Add("This hotel has been already presented.");
                logger.Warn($"{hotel.Title} has been already presented ");
            }

            if (errors.Count() == 0)
            {
                unitOfWork.Hotels.Create(hotelEntity);
            }

            return errors;
        }
       
        /// <summary>
        ///     Updates stored hotel entity
        /// </summary>
        /// <param name="hotel">New hotel information</param>
        /// <returns>Sequence of errors</returns>
        public IEnumerable<string> EditHotel(HotelDTO hotel)
        {
            var errors = new List<string>();

            hotel.Title = hotel.Title.Trim();

            if (unitOfWork.Hotels.Find(h => h.Title == hotel.Title).Any())
            {
                errors.Add("This hotel has been already presented.");
                logger.Warn($"{hotel.Title} has been already presented while editing.");
            }

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

        /// <summary>
        ///     Deletes hotel from the DB.
        /// </summary>
        /// <param name="id">Hotel identifier</param>
        /// <returns>Sequence of errors</returns>
        public IEnumerable<string> DeleteHotel(int id)
        {
            var result = new List<string>();

            var hotel = unitOfWork.Hotels.Get(id);

            if (unitOfWork.Tours.Find(t => t.Hotel.Id == id).Any())
            {
                result.Add("The hotel cannot be deleted, because some tours reference to it");
                logger.Warn($"{hotel.Title} cannot be deleted, because some tours reference to it ");
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
