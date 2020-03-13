using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using VeronaTour.DAL.Entites;

namespace VeronaTour.DAL.EF
{
    public class VeronaTourDbContext : IdentityDbContext<User>
    {
        public VeronaTourDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<VeronaTourDbContext>(null);

            this.Database.CreateIfNotExists();
            this.Database.Initialize(true);

            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourType> TourTypes { get; set; }
        public DbSet<FeedingType> FeedingTypes { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<SaleSettings> SaleSettings { get; set; }
    }
}
