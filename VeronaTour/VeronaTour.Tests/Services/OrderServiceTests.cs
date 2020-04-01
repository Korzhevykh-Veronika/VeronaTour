using AutoMapper;
using Moq;
using NLog;
using System.Collections.Generic;
using System.Linq;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;
using Xunit;

namespace VeronaTour.Tests.Services
{
    public class OrderServiceTests
    {
        private OrdersService ordersService;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IMapper> mapperMock;

        public OrderServiceTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();

            mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger>();

            ordersService = new OrdersService(unitOfWorkMock.Object, mapperMock.Object, loggerMock.Object);
        }

        [Fact]
        public void AddSale_DefaultSale_SaleWasCreated()
        {
            var salesInDb = new List<SaleSettings>()
            {
                new SaleSettings()
                {
                    MaxSale=25,
                    SaleStep=5
                }
            };

            unitOfWorkMock
                 .Setup(uow => uow.Sales.GetAll())
                 .Returns(salesInDb);

            var sale = new SaleSettingsDTO()
            {
                MaxSale = 40,
                SaleStep = 10
            };

            mapperMock
                .Setup(m => m.Map<SaleSettings>(It.IsAny<SaleSettingsDTO>()))
                .Returns(
                    new SaleSettings()
                    {
                        MaxSale = sale.MaxSale,
                        SaleStep = sale.SaleStep
                    });

            ordersService.AddSale(sale);

            unitOfWorkMock.Verify(uow => 
                uow.Sales.Create(
                    It.Is<SaleSettings>(ss => 
                        ss.MaxSale == sale.MaxSale && ss.SaleStep == sale.SaleStep)),
                    Times.Once());
        }

        [Fact]
        public void AddSale_DefaultSale_PreviousSaleWasDeleted()
        {
            var salesInDb = new List<SaleSettings>()
            {
                new SaleSettings()
                {
                    MaxSale=25,
                    SaleStep=5
                }
            };

            unitOfWorkMock
                 .Setup(uow => uow.Sales.GetAll())
                 .Returns(salesInDb);

            var sale = new SaleSettingsDTO()
            {
                MaxSale = 40,
                SaleStep = 10
            };

            ordersService.AddSale(sale);

            unitOfWorkMock.Verify(uow =>
                uow.Sales.Delete(
                    It.Is<int>(id => id == salesInDb.First().Id)),
                    Times.Once());

        }
    }
}
