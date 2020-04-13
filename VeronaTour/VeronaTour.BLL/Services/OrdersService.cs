using AutoMapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services.Interfaces;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;

namespace VeronaTour.BLL.Services
{
    /// <summary>
    ///     Contains business logic connected with identities
    /// </summary>
    public class OrdersService : IOrdersService
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILogger logger;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="newUnitOfWork">DB access provider</param>
        /// <param name="newMapper">Mapper</param>
        /// <param name="newLogger">Logger</param>
        public OrdersService(IUnitOfWork newUnitOfWork, IMapper newMapper, ILogger newLogger)
        {
            unitOfWork = newUnitOfWork;
            mapper = newMapper;
            logger = newLogger;
        }

        /// <summary>
        ///     Recalculates sale for the specific user
        /// </summary>
        /// <param name="user">User information</param>
        public void RecalculateSale(User user)
        {
            var settings = GetSaleSettings();

            var newSale = user.Sale + settings.SaleStep;

            if (newSale > settings.MaxSale)
            {
                newSale = settings.MaxSale;
            }

            user.Sale = newSale;

            logger.Debug($"New sale for user {user.UserName} was calculated to {user.Sale}%");
        }

        /// <summary>
        ///     Get current sale settings
        /// </summary>
        /// <returns>Sale settings</returns>
        public SaleSettingsDTO GetSaleSettings()
        {
            var settings = mapper.Map<SaleSettingsDTO>(unitOfWork.Sales.GetAll().FirstOrDefault());

            return settings;
        }

        /// <summary>
        ///     Creates order entity and saves into the DB.
        /// </summary>
        /// <param name="toutId">Tour identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="numberOfPeople">Selected number of people</param>
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

            logger.Info($"Order {order.Id} | {order.Tour.Title}-{order.User.UserName} was created.");
        }

        /// <summary>
        ///     Change order status
        /// </summary>
        /// <param name="orderId">Order identifier</param>
        /// <param name="status">New order status</param>
        public void ChangeOrderStatus(int orderId, string status)
        {
            var newstatus = unitOfWork.OrderStatuses.Find(s => s.Title == status).FirstOrDefault();
            var orderEntity = unitOfWork.Orders.Get(orderId);
            var actualSale = unitOfWork.Users.GetAll().SingleOrDefault(u => u.Id == orderEntity.User.Id).Sale;

            orderEntity.Status = newstatus;
            orderEntity.DateUpdateOrder = DateTime.Now;

            if (status == "Paid")
                orderEntity.FinalSale = actualSale;

            if (status == "Canceled")
                orderEntity.Tour.CountOfTour = orderEntity.Tour.CountOfTour + orderEntity.NumberOfPeople;
            
            unitOfWork.Orders.Update(orderEntity);

            logger.Info($"Order {orderEntity.Id} | {orderEntity.Tour.Title}-{orderEntity.User.UserName} "
                + $"has new status - {orderEntity.Status}");
        }

        /// <summary>
        ///     Changes default sale settings
        /// </summary>
        /// <param name="sale">New sale settings</param>
        public void AddSale(SaleSettingsDTO sale)
        {
            var saleEntity = mapper.Map<SaleSettings>(sale);

            var allPreviousSettings = unitOfWork.Sales.GetAll().FirstOrDefault();
            unitOfWork.Sales.Delete(allPreviousSettings.Id);

            unitOfWork.Sales.Create(saleEntity);

            logger.Info($"Default sale was set to Max - {sale.MaxSale} | Step - {sale.SaleStep}.");
        }

        /// <summary>
        ///     Get order information
        /// </summary>
        /// <param name="id">Order identiirier</param>
        /// <returns>Order information</returns>
        public OrderDTO GetOrder(int id)
        {
            return mapper.Map<OrderDTO>(unitOfWork.Orders.GetAll().SingleOrDefault(o => o.Id == id));
        }

        /// <summary>
        ///     Get all orders of specific user
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Sequence of orders</returns>
        public IEnumerable<OrderDTO> GetOrdersForUser(int userId)
        {
            return mapper.Map<IEnumerable<OrderDTO>>(unitOfWork.Orders.Find(order => order.User.UserId == userId).ToList());
        }

        /// <summary>
        ///     Get not registered orders for specific user
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Sequence of orders</returns>
        public IEnumerable<OrderDTO> GetNotRegisterOrdersForUser(int userId)
        {
            return mapper.Map<IEnumerable<OrderDTO>>(unitOfWork.Orders.Find(order => order.User.UserId == userId && order.Status.Id == 2).ToList());
        }

        /// <summary>
        ///     Get registered orders for user
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Sequence of orders</returns>
        public IEnumerable<OrderDTO> GetRegisterOrdersForUser(int userId)
        {
            return mapper.Map<IEnumerable<OrderDTO>>(unitOfWork.Orders.Find(order => order.User.UserId == userId && order.Status.Id != 2).ToList());
        }

        /// <summary>
        ///     Get all orders information
        /// </summary>
        /// <returns>Sequence of orders</returns>
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            return mapper.Map<IEnumerable<OrderDTO>>(unitOfWork.Orders.GetAll().Where(order => order.Status.Id != 2).OrderBy(o => o.Status.Id).ToList());
        }

        /// <summary>
        ///     Delete order from the DB.
        /// </summary>
        /// <param name="id">Order identifier</param>
        public void DeleteOrder(int id)
        {
            unitOfWork.Orders.Delete(id);
        }

        /// <summary>
        ///     Register orders
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Sequence of errors</returns>
        public IEnumerable<string> RegisterOrders(int userId)
        {
            var errors = new List<string>();

            var registerOrders = unitOfWork.Orders.Find(order => order.User.UserId == userId && order.Status.Title == "Not registered").ToList();

            var status = unitOfWork.OrderStatuses.Get(1);

            foreach (var ro in registerOrders)
            {
                if (ro.Tour.CountOfTour >= ro.NumberOfPeople)
                {
                    ro.Status = status;
                    ro.DateOrder = DateTime.Now;
                    ro.DateUpdateOrder = DateTime.Now;
                    ro.Tour.CountOfTour = ro.Tour.CountOfTour - ro.NumberOfPeople;
                    unitOfWork.Orders.Update(ro);
                    logger.Info($"Order #{ro.Id} was registered.");
                }
                else
                {
                    errors.Add($"{ro.Tour.Title} doesn't have enought places.");
                    logger.Warn($"Cannot register order #{ro.Id} | {ro.Tour.Title} doesn't have enought places.");
                }
                
            }            

            return errors;
        }

        /// <summary>
        ///     Get sum of not registered orders for user
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Sum</returns>
        public int GetTotalPriceForNotRegisterOrdersForUser(int id)
        {
            var orders = this.GetNotRegisterOrdersForUser(id);

            return Convert.ToInt32(orders.Select(o => o.NumberOfPeople * o.Tour.Price).Sum());
        }

        /// <summary>
        ///     Get sum of registered orders for user
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Sum</returns>
        public int GetTotalPriceForRegisterOrdersForUser(int id)
        {
            var orders = unitOfWork.Orders.Find(order => order.User.UserId == id && order.Status.Id == 1);

            return Convert.ToInt32(orders.Select(o => o.NumberOfPeople * o.Tour.Price).Sum());
        }
    }
}
