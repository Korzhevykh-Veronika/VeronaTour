using AutoMapper;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services.Interfaces;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;

namespace VeronaTour.BLL.Services
{
    /// <summary>
    ///     Contains business logic connected with tours
    /// </summary>
    public class ToursService : IToursService
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
        public ToursService(IUnitOfWork newUnitOfWork, IMapper newMapper, ILogger newLogger)
        {
            unitOfWork = newUnitOfWork;
            mapper = newMapper;
            logger = newLogger;
        }

        /// <summary>
        ///     Get all tours
        /// </summary>
        /// <returns>Sequence of tours</returns>
        public IEnumerable<TourDTO> GetTours()
        {
            return mapper.Map<IEnumerable<TourDTO>>(unitOfWork.Tours.GetAll().Where(t => !t.IsDeleted && t.CountOfTour > 0));
        }

        /// <summary>
        ///     Get all tours even soft deleted
        /// </summary>
        /// <returns>Sequence of tours</returns>
        public IEnumerable<TourDTO> GetSortAdminTours()
        {
            return mapper.Map<IEnumerable<TourDTO>>(unitOfWork.Tours.GetAll().OrderBy(t => t.IsDeleted).ThenByDescending(t => t.Id));
        }

        /// <summary>
        ///     Get available for selecting tours
        /// </summary>
        /// <returns>Sequence of tours</returns>
        public IEnumerable<TourDTO> GetSortTours()
        {
            var startDatetime = DateTime.Today.AddDays(1);
            var endDatetime = DateTime.Today.AddDays(31);

            var tours = unitOfWork
                .Tours
                .GetAll()
                .Where(t => !t.IsDeleted
                    && startDatetime <= t.StartDate
                    && endDatetime >= t.EndDate
                    && t.CountOfTour > 0)
                .OrderByDescending(t => t.HotTour)
                .ToList();

            return mapper.Map<IEnumerable<TourDTO>>(tours);
        }

        /// <summary>
        ///     Get specific tour
        /// </summary>
        /// <param name="id">Tour identifier</param>
        /// <param name="showAll">Should return deleted tours</param>
        /// <returns></returns>
        public TourDTO GetTour(int id, bool showAll)
        {
            var tour = unitOfWork.Tours.Get(id);

            if (tour == null 
                || (!showAll && (tour.IsDeleted || tour.CountOfTour <= 0)))
            {
                logger.Warn($"Tour with id {id} was not found.");

                return null;
            }
            
            tour.MaxPeopleCount = (tour.MaxPeopleCount < tour.CountOfTour)
                                               ? tour.MaxPeopleCount
                                               : tour.CountOfTour;

            tour.MinPeopleCount = (tour.MinPeopleCount < tour.CountOfTour)
                ? tour.MinPeopleCount
                : tour.CountOfTour;

            return mapper.Map<TourDTO>(tour);
        }

        /// <summary>
        ///     Get hot tours
        /// </summary>
        /// <returns>Sequence of tours</returns>
        public IEnumerable<TourDTO> GetHotTours()
        {
            var startDatetime = DateTime.Today.AddDays(1);

            return mapper.Map<IEnumerable<TourDTO>>(unitOfWork.Tours.Find(t => !t.IsDeleted && t.HotTour && startDatetime <= t.StartDate && t.CountOfTour > 0).ToList());
        }    

        /// <summary>
        ///     Get filtered by filter options tours
        /// </summary>
        /// <param name="filterOptions">Filter options</param>
        /// <returns>Sequence of tours</returns>
        public IEnumerable<TourDTO> GetFilteredTours(FilterDTO filterOptions)
        {
            var tours = unitOfWork.Tours.Find(t => !t.IsDeleted && t.CountOfTour > 0).ToList();
            var hotel = unitOfWork.Hotels.GetAll().ToList();
            var feedingTypes = unitOfWork.FeedingTypes.GetAll();

            IEnumerable<int> selectedFeedingTypesIds;

            if (filterOptions.CheckedFood.Any(cf => cf))
            {
                selectedFeedingTypesIds = feedingTypes.Where((t, index) => filterOptions.CheckedFood[index]).Select(ft => ft.Id);
            }
            else
            {
                selectedFeedingTypesIds = feedingTypes.Select(ft => ft.Id);
            }

            IEnumerable<int> selectedHotelIds;

            if (filterOptions.CheckedHotel.Any(cf => cf))
            {
                selectedHotelIds = hotel.Where((h) => filterOptions.CheckedHotel[h.StarsCount - 1]).Select(ft => ft.Id);
            }
            else
            {
                selectedHotelIds = hotel.Select(ft => ft.Id);
            }

            if (!String.IsNullOrEmpty(filterOptions.SelectedCountry)
                && !filterOptions.SelectedCountry.Equals(variantAll))
            {
                tours = tours.Where(t => t.Country.Title == filterOptions.SelectedCountry).ToList();
            }
            if (!String.IsNullOrEmpty(filterOptions.SelectedTourType)
                && !filterOptions.SelectedTourType.Equals(variantAll))
            {
                tours = tours.Where(t => t.Type.Title == filterOptions.SelectedTourType).ToList();
            }
            if (!String.IsNullOrEmpty(filterOptions.SelectedStartDate))
            {
                tours = tours.Where(t => DateTime.ParseExact(filterOptions.SelectedStartDate, "MM/dd/yyyy", null) <= t.StartDate).ToList();
            }
            if (!String.IsNullOrEmpty(filterOptions.SelectedEndDate))
            {
                tours = tours.Where(t => DateTime.ParseExact(filterOptions.SelectedEndDate, "MM/dd/yyyy", null) >= t.EndDate).ToList();
            }
            if (filterOptions.SelectedNumberOfPeople != 0)
            {
                tours = tours
                    .Where(t => 
                        t.MinPeopleCount <= filterOptions.SelectedNumberOfPeople 
                        && filterOptions.SelectedNumberOfPeople <= t.MaxPeopleCount)
                    .ToList();
            }
            if (filterOptions.SelectedPrice != 0)
            {
                tours = tours.Where(t => t.Price <= filterOptions.SelectedPrice).ToList();
            }
            if (filterOptions.CheckedFood != null)
            {
                tours = tours.Where(t => selectedFeedingTypesIds.Contains(t.FeedingType.Id)).ToList();
            }
            if (filterOptions.CheckedHotel != null)
            {
                tours = tours.Where(t => selectedHotelIds.Contains(t.Hotel.Id)).ToList();
            }

            return mapper.Map<IEnumerable<TourDTO>>(tours);
        }

        /// <summary>
        ///     Adds tour to the database
        /// </summary>
        /// <param name="tour">Tour information</param>
        /// <param name="selectedHotel">Selected hotel</param>
        /// <param name="selectedFeedingType">Selected feeding type</param>
        /// <param name="selectedTourType">Selected tour type</param>
        /// <param name="selectedCountry">Selected country</param>
        /// <param name="uploadImage">Uploaded image</param>
        /// <returns>Sequence of errors</returns>
        public IEnumerable<string> AddTour(
            TourDTO tour,
            string selectedHotel,
            string selectedFeedingType,
            string selectedTourType,
            string selectedCountry,
            HttpPostedFileBase uploadImage)
        {
            var errors = new List<string>();

            var tourEntity = mapper.Map<Tour>(tour);

            if (unitOfWork.Tours.Find(t => t.Title == tour.Title).Any())
            {
                errors.Add("This tour has been already presented.");
                logger.Warn($"{tour.Title} has been already presented");
            }

            if (errors.Count() == 0)
            {
                tourEntity.Hotel = unitOfWork.Hotels.Find(h => selectedHotel.Contains(h.Title)).FirstOrDefault();
                tourEntity.FeedingType = unitOfWork.FeedingTypes.Find(ft => ft.Title == selectedFeedingType).FirstOrDefault();
                tourEntity.Type = unitOfWork.TourTypes.Find(h => h.Title == selectedTourType).FirstOrDefault();
                tourEntity.Country = unitOfWork.Countries.Find(h => h.Title == selectedCountry).FirstOrDefault();

                byte[] imageData = null;
                
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                
                tourEntity.Image = imageData;

                unitOfWork.Tours.Create(tourEntity);

                logger.Info($"Tour {tourEntity.Title} was added.");
            }

            return errors;
        }

        /// <summary>
        ///     Updates tour entity
        /// </summary>
        /// <param name="tour">Tour information</param>
        /// <param name="selectedHotel">Selected hotel</param>
        /// <param name="selectedFeedingType">Selected feeding type</param>
        /// <param name="selectedTourType">Selected tour type</param>
        /// <param name="selectedCountry">Selected country</param>
        /// <param name="uploadImage">Uploaded image</param>
        /// <returns>Sequence of errors</returns>
        public IEnumerable<string> EditTour(
            TourDTO tour,
            string selectedHotel,
            string selectedFeedingType,
            string selectedTourType,
            string selectedCountry,
            HttpPostedFileBase uploadImage)
        {
            var errors = new List<string>();

            var tourEntity = unitOfWork.Tours.Get(tour.Id);

            if (errors.Count() == 0)
            {
                tourEntity.HotTour = tour.HotTour;
                tourEntity.MaxPeopleCount = tour.MaxPeopleCount;
                tourEntity.MinPeopleCount = tour.MinPeopleCount;
                tourEntity.Price = tour.Price;
                tourEntity.StartDate = tour.StartDate;
                tourEntity.Title = tour.Title;
                tourEntity.EndDate = tour.EndDate;
                tourEntity.Description = tour.Description;
                tourEntity.CountOfTour = tour.CountOfTour;

                tourEntity.HotTour = tour.HotTour;
                tourEntity.Hotel = unitOfWork.Hotels.Find(h => selectedHotel.Contains(h.Title)).FirstOrDefault();
                tourEntity.FeedingType = unitOfWork.FeedingTypes.Find(ft => ft.Title == selectedFeedingType).FirstOrDefault();
                tourEntity.Type = unitOfWork.TourTypes.Find(h => h.Title == selectedTourType).FirstOrDefault();
                tourEntity.Country = unitOfWork.Countries.Find(h => h.Title == selectedCountry).FirstOrDefault();

                if (uploadImage != null)
                {
                    byte[] imageData = null;

                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }

                    tourEntity.Image = imageData;
                }

                unitOfWork.Tours.Update(tourEntity);

                logger.Info($"Tour {tourEntity.Title} was edited.");
            }

            return errors;
        }

        /// <summary>
        ///     Deletes tour from the DB.
        /// </summary>
        /// <param name="id">Tour identifier</param>
        public void DeleteTour(int id)
        {
            var tour = unitOfWork.Tours.Get(id);

            if (tour != null)
            {
                tour.IsDeleted = true;
                unitOfWork.Tours.Update(tour);

                logger.Info($"Tour {tour.Title} was soft deleted.");
            }
            else
            {
                logger.Warn($"Tour {tour.Title} was not found for deleting.");
            }
        }

        /// <summary>
        ///     Change hot status of tour to vise versa
        /// </summary>
        /// <param name="id">Tour identifier</param>
        public void ChangeTourHot(int id)
        {
            var tour = unitOfWork.Tours.Find(t => t.Id == id).FirstOrDefault();

            tour.HotTour = !tour.HotTour;

            unitOfWork.Tours.Update(tour);

            logger.Info($"Tour {tour.Title} is " + (tour.HotTour ? "" : "not ") + "hot.");
        }
    }
}
