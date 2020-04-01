using VeronaTour.DAL.Entites;

namespace VeronaTour.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<FeedingType> FeedingTypes { get; }
        IRepository<Tour> Tours { get; }
        IRepository<TourType> TourTypes { get; }
        IRepository<Hotel> Hotels { get; }
        IRepository<User> Users { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderStatus> OrderStatuses { get; }
        IRepository<Country> Countries { get; }
        IRepository<SaleSettings> Sales { get; }
        IRepository<ExceptionDetail> Logs { get; }
    }
}
