using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeronaTour.DAL.EF;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;
using VeronaTour.DAL.Repositories;

namespace VeronaTour.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private VeronaTourDbContext dbContext;
        private IRepository<FeedingType> feedingTypeRepository;
        private IRepository<Hotel> hotelRepository;
        private IRepository<Order> orderRepository;
        private IRepository<OrderStatus> orderStatusRepository;
        private IRepository<Tour> tourRepository;
        private IRepository<TourType> tourTypeRepository;
        private IRepository<User> userRepository;
        //private IRepository<UserRole> userRoleRepository;
        public IRepository<Country> countryRepository;
        public IRepository<SaleSettings> saleRepository;


        public UnitOfWork(VeronaTourDbContext veronaTourDbContext)
        {
            dbContext = veronaTourDbContext;
            feedingTypeRepository = new GeneralRepository<FeedingType>(dbContext);
            hotelRepository = new GeneralRepository<Hotel>(dbContext);
            orderRepository = new GeneralRepository<Order>(dbContext);
            orderStatusRepository = new GeneralRepository<OrderStatus>(dbContext);
            tourRepository = new GeneralRepository<Tour>(dbContext);
            tourTypeRepository= new GeneralRepository<TourType>(dbContext);
            userRepository = new GeneralRepository<User>(dbContext);
            //userRoleRepository = new GeneralRepository<UserRole>(dbContext);
            countryRepository = new GeneralRepository<Country>(dbContext);
            saleRepository = new GeneralRepository<SaleSettings>(dbContext);
        }

        public IRepository<SaleSettings> Sales
        {
            get { return saleRepository; }
        }
        public IRepository<Country> Countries
        {
            get { return countryRepository; }
        }

        public IRepository<FeedingType> FeedingTypes
        {
            get { return feedingTypeRepository; }
        }

        public IRepository<Hotel> Hotels
        {
            get { return hotelRepository; }
        }

        public IRepository<Order> Orders
        {
            get { return orderRepository; }
        }

        public IRepository<OrderStatus> OrderStatuses
        {
            get { return orderStatusRepository; }
        }

        public IRepository<Tour> Tours
        {
            get { return tourRepository; }
        }

        public IRepository<TourType> TourTypes
        {
            get { return tourTypeRepository; }
        }

        public IRepository<User> Users
        {
            get { return userRepository; }
        }

        //public IRepository<UserRole> UserRoles
        //{
        //    get { return userRoleRepository; }
        //}


        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}