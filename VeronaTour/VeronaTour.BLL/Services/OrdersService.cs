using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services.Interfaces;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;

namespace VeronaTour.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;

        const string variantAll = "Choose...";

        public OrdersService(IUnitOfWork newUnitOfWork, IMapper newMapper)
        {
            unitOfWork = newUnitOfWork;
            mapper = newMapper;
        }

        public void RecalculateSale(User user)
        {
            var settings = GetSaleSettings();

            var newSale = user.Sale + settings.SaleStep;

            if (newSale > settings.MaxSale)
            {
                newSale = settings.MaxSale;
            }

            user.Sale = newSale;
        }
        public SaleSettingsDTO GetSaleSettings()
        {
            var settings = mapper.Map<SaleSettingsDTO>(unitOfWork.Sales.GetAll().FirstOrDefault());

            //if (settings == null) settings = new SaleSettingsDTO() { MaxSale = 25, SaleStep = 5 };

            return settings;
        }

        public void CreateOrder(int toutId, int userId, int numberOfPeople)
        {
            var order = new Order
            {
                Tour = unitOfWork.Tours.Get(toutId),
                NumberOfPeople = numberOfPeople,

                User = unitOfWork.Users.Find(user => user.UserId == userId).First(),
                Status = unitOfWork.OrderStatuses.Get(2)
            };

            unitOfWork.Orders.Create(order);
        }

        public void ChangeOrderStatus(int orderId, string status)
        {
            var newstatus = unitOfWork.OrderStatuses.Find(s => s.Title == status).FirstOrDefault();
            var orderEntity = unitOfWork.Orders.Get(orderId);
            orderEntity.Status = newstatus;
            orderEntity.DateUpdateOrder = DateTime.Now;
            if (status == "Paid") orderEntity.FinalSale = orderEntity.User.Sale;
            if (status == "Canceled") orderEntity.Tour.CountOfTour = orderEntity.Tour.CountOfTour + orderEntity.NumberOfPeople;
            unitOfWork.Orders.Update(orderEntity);
        }

        public void AddSale(SaleSettingsDTO sale)
        {
            var saleEntity = mapper.Map<SaleSettings>(sale);

            var allPreviousSettings = unitOfWork.Sales.GetAll().FirstOrDefault();
            unitOfWork.Sales.Delete(allPreviousSettings.Id);

            unitOfWork.Sales.Create(saleEntity);
        }

        public OrderDTO GetOrder(int id)
        {
            return mapper.Map<OrderDTO>(unitOfWork.Orders.Get(id));
        }
        public IEnumerable<OrderDTO> GetOrdersForUser(int userId)
        {
            return mapper.Map<IEnumerable<OrderDTO>>(unitOfWork.Orders.Find(order => order.User.UserId == userId).ToList());
        }
        public IEnumerable<OrderDTO> GetNotRegisterOrdersForUser(int userId)
        {
            return mapper.Map<IEnumerable<OrderDTO>>(unitOfWork.Orders.Find(order => order.User.UserId == userId && order.Status.Id == 2).ToList());
        }
        public IEnumerable<OrderDTO> GetRegisterOrdersForUser(int userId)
        {
            return mapper.Map<IEnumerable<OrderDTO>>(unitOfWork.Orders.Find(order => order.User.UserId == userId && order.Status.Id != 2).ToList());
        }
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            return mapper.Map<IEnumerable<OrderDTO>>(unitOfWork.Orders.Find(order => order.Status.Id != 2).OrderBy(o => o.Status.Id).ToList());
        }
        public void DeleteOrder(int id)
        {
            unitOfWork.Orders.Delete(id);
        }

        public IEnumerable<string> RegisterOrders(int userId)
        {
            var errors = new List<string>();

            var registerOrders = unitOfWork.Orders.Find(order => order.User.UserId == userId && order.Status.Title == "Not registered");

            var status = unitOfWork.OrderStatuses.Get(1);

            foreach (var ro in registerOrders)
            {
                if (ro.Tour.CountOfTour >= ro.NumberOfPeople)
                {
                    ro.Status = status;
                    ro.DateOrder = DateTime.Now;
                    ro.DateUpdateOrder = DateTime.Now;
                    ro.Tour.CountOfTour = ro.Tour.CountOfTour - ro.NumberOfPeople;
                }
                else
                {
                    errors.Add($"{ro.Tour.Title} doesn't have enought places.");
                }

            }

            unitOfWork.Orders.Update(registerOrders);
            return errors;
        }

        public int GetTotalPriceForNotRegisterOrdersForUser(int id)
        {
            var orders = this.GetNotRegisterOrdersForUser(id);

            return Convert.ToInt32(orders.Select(o => o.NumberOfPeople * o.Tour.Price).Sum());
        }

        public int GetTotalPriceForRegisterOrdersForUser(int id)
        {
            var orders = unitOfWork.Orders.Find(order => order.User.UserId == id && order.Status.Id == 1);

            return Convert.ToInt32(orders.Select(o => o.NumberOfPeople * o.Tour.Price).Sum());
        }
    }
}
