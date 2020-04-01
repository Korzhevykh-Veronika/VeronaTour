using AutoMapper;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;
using Xunit;

namespace VeronaTour.Tests.Services
{
    public class MainServicesTests
    {
        private MainService mainService;
        private Mock<IUnitOfWork> unitOfWorkMock;

        public MainServicesTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger>();

            mainService = new MainService(unitOfWorkMock.Object, mapperMock.Object, loggerMock.Object);
        }

        [Fact]
        public void AddCountry_ExistingCountry_ErrorReturned()
        {
            var countriesInDb = new List<Country>()
            {
                new Country()
                {
                    Title = "USA"
                }
            };

           unitOfWorkMock
                .Setup(uow => uow.Countries.Find(It.IsAny<Func<Country, bool>>()))
                .Returns(countriesInDb);

            var country = new CountryDTO()
            {
                Title = "USA"
            };

            var errors = mainService.AddCountry(country);

            Assert.True(errors.Any());
        }

        [Fact]
        public void AddTourType_ExistingTourType_ErrorReturned()
        {
            var existingTourTypes = new List<TourType>()
            {
                new TourType()
                {
                    Title = "Bus"
                }
            };

            var tourTypeRepositoryMock = new Mock<IRepository<TourType>>();
            tourTypeRepositoryMock.Setup(cr => cr.Find(It.IsAny<Func<TourType, bool>>())).Returns(existingTourTypes);
            unitOfWorkMock.Setup(uow => uow.TourTypes).Returns(tourTypeRepositoryMock.Object);

            var tourType = new TourTypeDTO()
            {
                Title = "Bus"
            };

            var errors = mainService.AddTourType(tourType);

            Assert.True(errors.Any());
        }

        [Fact]
        public void AddHotel_ExistingHotel_ErrorReturned()
        {
            var hotelsInDb = new List<Hotel>()
            {
                new Hotel()
                {
                    Title = "Palace"
                }
            };

            var hotelsRepositoryMock = new Mock<IRepository<Hotel>>();
            hotelsRepositoryMock.Setup(cr => cr.Find(It.IsAny<Func<Hotel, bool>>())).Returns(hotelsInDb);
            unitOfWorkMock.Setup(uow => uow.Hotels).Returns(hotelsRepositoryMock.Object);

            var hotel = new HotelDTO()
            {
                Title = "USA"
            };

            var result = mainService.AddHotel(hotel);

            Assert.True(result.Any());
        }

        [Fact]
        public void DeleteHotel_TourReferencesToHotel_ErrorReturned()
        {
            var hotelInDb = new Hotel()
            {
                Id = 0,
                Title = "Palace hotel"
            };

            var toursInDb = new List<Tour>()
            {
                new Tour()
                {
                    Title = "Palace",
                    Hotel = hotelInDb
                }
            };

            var toursRepositoryMock = new Mock<IRepository<Tour>>();
            toursRepositoryMock.Setup(cr => cr.Find(It.IsAny<Func<Tour, bool>>())).Returns(toursInDb);

            var hotelRepositoryMock = new Mock<IRepository<Hotel>>();
            hotelRepositoryMock.Setup(cr => cr.Get(It.IsAny<int>())).Returns(hotelInDb);

            unitOfWorkMock.Setup(uow => uow.Tours).Returns(toursRepositoryMock.Object);
            unitOfWorkMock.Setup(uow => uow.Hotels).Returns(hotelRepositoryMock.Object);

            var errors = mainService.DeleteHotel(toursInDb.First().Hotel.Id);

            Assert.True(errors.Any());
        }
    }
}
