namespace VeronaTour.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VeronaTour.DAL.Entites;

    internal sealed class Configuration : DbMigrationsConfiguration<VeronaTour.DAL.EF.VeronaTourDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "VeronaTour.DAL.EF.VeronaTourDbContext";
        }

        protected override void Seed(VeronaTour.DAL.EF.VeronaTourDbContext db)
        {
            db.FeedingTypes.AddOrUpdate(new FeedingType { Id = 1, Title = "OB" });
            db.FeedingTypes.AddOrUpdate(new FeedingType { Id = 2, Title = "BB" });
            db.FeedingTypes.AddOrUpdate(new FeedingType { Id = 3, Title = "HB" });
            db.FeedingTypes.AddOrUpdate(new FeedingType { Id = 4, Title = "FB" });
            db.FeedingTypes.AddOrUpdate(new FeedingType { Id = 5, Title = "AI" });
            db.FeedingTypes.AddOrUpdate(new FeedingType { Id = 6, Title = "UAI" });


            db.OrderStatuses.AddOrUpdate(new OrderStatus { Id = 1, Title = "Registered" });
            db.OrderStatuses.AddOrUpdate(new OrderStatus { Id = 2, Title = "Not registered" });
            db.OrderStatuses.AddOrUpdate(new OrderStatus { Id = 3, Title = "Paid" });
            db.OrderStatuses.AddOrUpdate(new OrderStatus { Id = 4, Title = "Canceled" });

            db.TourTypes.AddOrUpdate(new TourType { Id = 1, Title = "Sea holiday" });
            db.TourTypes.AddOrUpdate(new TourType { Id = 2, Title = "Bus tour" });
            db.TourTypes.AddOrUpdate(new TourType { Id = 3, Title = "Shopping" });

            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 1,
                Title = "Pera Tulip Hotel",
                Description = "This design hotel is located in the Beyoglu district. Tulip By Molton Hotels is just a few steps from Sishane MRT Station, Nostalgic and Tunnel Tram Stations, providing easy access to Sultanahmet and the rest of Istanbul. Tulip By Molton Hotels has been welcoming guests to its comfortable rooms since 2008. The hotel is perfect for both sightseeing and business trips.",
                StarsCount = 1
            });

            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 2,
                Title = "Ferman Apart Hotel",
                Description = "The aparthotel is located in Istanbul, 800 meters from the Congress Center, 1.9 km from Taksim Square, 2.5 km from Istiklal Street and 2.5 km from Dolmabahce Palace. Dolmabahce Clock Tower is 3 km from the hotel, while Galata Tower is 4.4 km away. Suitable for business tours, romantic getaways.",
                StarsCount = 1
            });

            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 3,
                Title = "Arts Hotel",
                Description = "Arts Istanbul Design Hotel is 350 meters from Osmanbey Metro Station and a 10-minute walk from Taksim Square and the fashionable Nishantashi district. Perfect for both sightseeing and business trips.",
                StarsCount = 1
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 4,
                Title = "Florya Park Hotel",
                Description = "Florya Park Hotel is located near Ataturk International Airport and Istanbul Exhibition Center.Bakırköy is a great choice for travelers interested in food, culture and local cuisine.",
                StarsCount = 2
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 5,
                Title = "Maya Hotel",
                Description = "Maya Hotel is located in the center of the historical, tourist, shopping and entertainment district of Laleli. Located in the center of Istanbul, guests can visit the Great Bazaar, Blue Mosque, Hagia Sophia, located just a 10-minute walk from the hotel. In AddOrUpdateition, the hotel offers numerous restaurants, shopping and entertainment centers of the city. Vezneciler Metro Station, which provides easy access to other city attractions, is within walking distance.",
                StarsCount = 2
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 6,
                Title = "Sun Comfort Hotel",
                Description = "Sun Comfort Hotel is located in the center of the Kumkapi quarter of Fatih, about 1.5 km from historical sites such as the Blue Mosque, Hagia Sophia and Topkapi Sultan's Palace. Fatih is a great choice for travelers interested in monuments, architecture and the old city.",
                StarsCount = 3
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 7,
                Title = "Cihangir Hotel",
                Description = "Cihangir Hotel is located in Istanbul overlooking the Bosphorus. The hotel was completely renovated in 2013. The lively Taksim district with the nearest metro station is 300 meters away.",
                StarsCount = 3
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 8,
                Title = "Grand Mark Hotel",
                Description = "The hotel is located in the Fatih district,just a kilometer from the center of Istanbul.From here it is easy to get to all the main attractions of the city.",
                StarsCount = 4
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 9,
                Title = "Grand Liza Hotel",
                Description = "The comfortable 4-star hotel is located in the Kumkapi district, close to a palace, a basilica and a church.",
                StarsCount = 4
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 10,
                Title = "Sed Hotel",
                Description = "Hotel Sed is 300 meters from the Kabatas Tram Stop, which provides easy access to the old town. It offers air-conditioned rooms with a flat-screen TV, free Wi-Fi and a terrace with panoramic sea views.",
                StarsCount = 4
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 11,
                Title = "Abel Hotel",
                Description = "The hotel was opened in 2012,consists of one 7 - storey building.Located in the lively district of Istanbul,right next to the oriental bazaar, shops and 1.8 km from the Blue Mosque.",
                StarsCount = 5
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 12,
                Title = "Raimond Hotel",
                Description = "Raimond Hotel is located in a quiet area, just a 10-minute walk from the Blue Mosque. Raimond Hotel has welcomed guests in Istanbul since 2018. Fatih is a great choice if you are interested in the old city, architecture and history.",
                StarsCount = 5
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 13,
                Title = "Alphonse",
                Description = "Alphonse Hotel is located in the Kumkapi area, which is popular for its many seafood restaurants. Fatih is a great choice if you are interested in the old city, architecture and monuments.",
                StarsCount = 5
            });
            db.Hotels.AddOrUpdate(new Hotel
            {
                Id = 14,
                Title = "The Ottoman City Hotel ",
                Description = "Ottoman City Hotel is located in Istanbul.Nearby are the Onur Süper Market and the supermarket.Majidiyokoy Square is a 9 - minute drive away and Florence Nightingale Sisli Hospital is a 14 - minute drive away.Suitable for families.",
                StarsCount = 5
            });

            db.Countries.AddOrUpdate(new Country { Id = 1, Title = "Turkey" });
            db.Countries.AddOrUpdate(new Country { Id = 2, Title = "Ukraine" });
            db.Countries.AddOrUpdate(new Country { Id = 3, Title = "Egypt" });
            db.Countries.AddOrUpdate(new Country { Id = 4, Title = "Argentina" });

            db.SaleSettings.AddOrUpdate(new SaleSettings { Id = 1, MaxSale = 25, SaleStep = 5 });

            db.SaveChanges();
        }
    }
}
