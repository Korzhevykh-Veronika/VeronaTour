using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeronaTour.BLL.DTOs;
using VeronaTour.DAL.Entites;

namespace VeronaTour.BLL.Services.Interfaces
{
    public interface IOrdersService
    {
        void RecalculateSale(User user);
        void CreateOrder(int toutId, int userId, int numberOfPeople);
        IEnumerable<OrderDTO> GetOrdersForUser(int userId);
        IEnumerable<OrderDTO> GetNotRegisterOrdersForUser(int userId);
        IEnumerable<OrderDTO> GetRegisterOrdersForUser(int userId);
        IEnumerable<OrderDTO> GetAllOrders();
        void AddSale(SaleSettingsDTO sale);
        void ChangeOrderStatus(int id, string status);
        OrderDTO GetOrder(int id);
        void DeleteOrder(int id);

        int GetTotalPriceForNotRegisterOrdersForUser(int id);
        IEnumerable<string> RegisterOrders(int userId);
        int GetTotalPriceForRegisterOrdersForUser(int id);
        SaleSettingsDTO GetSaleSettings();

    }
}
