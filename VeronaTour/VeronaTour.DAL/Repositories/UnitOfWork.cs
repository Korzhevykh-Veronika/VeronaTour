using NLog;
using System;
using VeronaTour.DAL.EF;
using VeronaTour.DAL.Entites;
using VeronaTour.DAL.Interfaces;
using VeronaTour.DAL.Repositories;

namespace VeronaTour.DAL
{
    /// <summary>
    ///     Unit that provides access to repositories and manages their lifecycle
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly VeronaTourDbContext dbContext;
        private readonly IRepository<FeedingType> feedingTypeRepository;
        private readonly IRepository<Hotel> hotelRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<OrderStatus> orderStatusRepository;
        private readonly IRepository<Tour> tourRepository;
        private readonly IRepository<TourType> tourTypeRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<SaleSettings> saleRepository;
        private readonly IRepository<ExceptionDetail> exceptionDetail;
        
        /// <summary>
        ///     Constructor that creates repositories with same dbContext and logger
        /// </summary>
        /// <param name="veronaTourDbContext">Database context</param>
        /// <param name="logger">Logger</param>
        public UnitOfWork(VeronaTourDbContext veronaTourDbContext, ILogger logger)
        {
            dbContext = veronaTourDbContext;
            feedingTypeRepository = new GeneralRepository<FeedingType>(dbContext, logger);
            hotelRepository = new GeneralRepository<Hotel>(dbContext, logger);
            orderRepository = new GeneralRepository<Order>(dbContext, logger);
            orderStatusRepository = new GeneralRepository<OrderStatus>(dbContext, logger);
            tourRepository = new GeneralRepository<Tour>(dbContext, logger);
            tourTypeRepository= new GeneralRepository<TourType>(dbContext, logger);
            userRepository = new GeneralRepository<User>(dbContext, logger);
            countryRepository = new GeneralRepository<Country>(dbContext, logger);
            saleRepository = new GeneralRepository<SaleSettings>(dbContext, logger);
            exceptionDetail = new GeneralRepository<ExceptionDetail>(dbContext, logger);
        }

        public IRepository<SaleSettings> Sales
        {
            get { return saleRepository; }
        }

        public IRepository<ExceptionDetail> Logs
        {
            get { return exceptionDetail; }
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
        
        public void Dispose()
        {
            if(dbContext != null)
            {
                dbContext.Dispose();
            }            
        }
    }
}