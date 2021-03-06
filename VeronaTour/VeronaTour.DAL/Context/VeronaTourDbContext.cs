﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using VeronaTour.DAL.Entites;

namespace VeronaTour.DAL.EF
{
    public class VeronaTourDbContext : IdentityDbContext<User>
    {
        public VeronaTourDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<VeronaTourDbContext>(new CreateDatabaseIfNotExists<VeronaTourDbContext>());

            this.Database.Initialize(true);

            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourType> TourTypes { get; set; }
        public DbSet<FeedingType> FeedingTypes { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<SaleSettings> SaleSettings { get; set; }
        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
    }
}
