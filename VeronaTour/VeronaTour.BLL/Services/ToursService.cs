using AutoMapper;
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
    public class ToursService : IToursService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;

        const string variantAll = "Choose...";

        public ToursService(IUnitOfWork newUnitOfWork, IMapper newMapper)
        {
            unitOfWork = newUnitOfWork;
            mapper = newMapper;
        }

        public IEnumerable<TourDTO> GetTours()
        {
            return mapper.Map<IEnumerable<TourDTO>>(unitOfWork.Tours.GetAll().Where(t => !t.IsDeleted && t.CountOfTour > 0));

        }
        public IEnumerable<TourDTO> GetSortAdminTours()
        {
            return mapper.Map<IEnumerable<TourDTO>>(unitOfWork.Tours.GetAll().OrderBy(t => t.IsDeleted).ThenByDescending(t => t.Id));
        }
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
        public TourDTO GetTour(int id)
        {
            var tour = unitOfWork.Tours.Get(id);

            tour.MaxPeopleCount = (tour.MaxPeopleCount < tour.CountOfTour) // to service
                    ? tour.MaxPeopleCount
                    : tour.CountOfTour;

            tour.MinPeopleCount = (tour.MinPeopleCount < tour.CountOfTour)
                ? tour.MinPeopleCount
                : tour.CountOfTour;

            return mapper.Map<TourDTO>(tour);
        }

        public IEnumerable<TourDTO> GetHotTours()
        {
            var startDatetime = DateTime.Today.AddDays(1);

            return mapper.Map<IEnumerable<TourDTO>>(unitOfWork.Tours.Find(t => !t.IsDeleted && t.HotTour && startDatetime <= t.StartDate && t.CountOfTour > 0).ToList());
        }    

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
                tours = tours.Where(t => t.MinPeopleCount <= filterOptions.SelectedNumberOfPeople && filterOptions.SelectedNumberOfPeople <= t.MaxPeopleCount).ToList();

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
            }

            if (errors.Count() == 0)
            {
                tourEntity.Hotel = unitOfWork.Hotels.Find(h => selectedHotel.Contains(h.Title)).FirstOrDefault();
                tourEntity.FeedingType = unitOfWork.FeedingTypes.Find(ft => ft.Title == selectedFeedingType).FirstOrDefault();
                tourEntity.Type = unitOfWork.TourTypes.Find(h => h.Title == selectedTourType).FirstOrDefault();
                tourEntity.Country = unitOfWork.Countries.Find(h => h.Title == selectedCountry).FirstOrDefault();

                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                tourEntity.Image = imageData;


                unitOfWork.Tours.Create(tourEntity);
            }

            return errors;
        }

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
            }

            return errors;
        }

        public void DeleteTour(int id)
        {
            var tour = unitOfWork.Tours.Get(id);
            if (tour != null)
            {
                tour.IsDeleted = true;
                unitOfWork.Tours.Update(tour);
            }

        }

        public void ChangeTourHot(int id)
        {
            var tour = unitOfWork.Tours.Find(t => t.Id == id).FirstOrDefault();

            tour.HotTour = !tour.HotTour;

            unitOfWork.Tours.Update(tour);
        }
    }
}
