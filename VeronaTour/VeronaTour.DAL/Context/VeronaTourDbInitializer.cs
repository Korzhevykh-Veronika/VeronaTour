using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeronaTour.DAL.Entites;

namespace VeronaTour.DAL.EF
{
    public class VeronaTourDbInitializer : DropCreateDatabaseAlways<VeronaTourDbContext>
    {
        protected override void Seed(VeronaTourDbContext db)
        {
            //var FeedingType1 = new FeedingType { Title = "OB" };
            //db.FeedingTypes.Add(FeedingType1);
            //var FeedingType2 = new FeedingType { Title = "BB" };
            //db.FeedingTypes.Add(FeedingType2);
            //var FeedingType3 = new FeedingType { Title = "HB" };
            //db.FeedingTypes.Add(FeedingType3);
            //var FeedingType4 = new FeedingType { Title = "FB" };
            //db.FeedingTypes.Add(FeedingType4);
            //var FeedingType5 = new FeedingType { Title = "AI" };
            //db.FeedingTypes.Add(FeedingType5);
            //var FeedingType6 = new FeedingType { Title = "UAI" };
            //db.FeedingTypes.Add(FeedingType6);


            //db.OrderStatuses.Add(new OrderStatus { Title = "Registered" });
            //db.OrderStatuses.Add(new OrderStatus { Title = "Not registered" });
            //db.OrderStatuses.Add(new OrderStatus { Title = "Paid" });
            //db.OrderStatuses.Add(new OrderStatus { Title = "Canceled" });

            //var TourType1 = new TourType { Title = "Sea holiday" };
            //db.TourTypes.Add(TourType1);
            //var TourType2 = new TourType { Title = "Bus tour" };
            //db.TourTypes.Add(TourType2);
            //var TourType3 = new TourType { Title = "Shopping" };
            //db.TourTypes.Add(TourType3);

            //var defaultHotel = new Hotel
            //{
            //    Title = "Pera Tulip Hotel",
            //    Description = "This design hotel is located in the Beyoglu district. Tulip By Molton Hotels is just a few steps from Sishane MRT Station, Nostalgic and Tunnel Tram Stations, providing easy access to Sultanahmet and the rest of Istanbul. Tulip By Molton Hotels has been welcoming guests to its comfortable rooms since 2008. The hotel is perfect for both sightseeing and business trips.",
            //    StarsCount = 1
            //};
            //db.Hotels.Add(defaultHotel);

            //var Hotel2 = new Hotel
            //{
            //    Title = "Ferman Apart Hotel",
            //    Description = "The aparthotel is located in Istanbul, 800 meters from the Congress Center, 1.9 km from Taksim Square, 2.5 km from Istiklal Street and 2.5 km from Dolmabahce Palace. Dolmabahce Clock Tower is 3 km from the hotel, while Galata Tower is 4.4 km away. Suitable for business tours, romantic getaways.",
            //    StarsCount = 1
            //};
            //db.Hotels.Add(Hotel2);
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Arts Hotel",
            //    Description = "Arts Istanbul Design Hotel is 350 meters from Osmanbey Metro Station and a 10-minute walk from Taksim Square and the fashionable Nishantashi district. Perfect for both sightseeing and business trips.",
            //    StarsCount = 1
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Florya Park Hotel",
            //    Description = "Florya Park Hotel is located near Ataturk International Airport and Istanbul Exhibition Center.Bakırköy is a great choice for travelers interested in food, culture and local cuisine.",
            //    StarsCount = 2
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Maya Hotel",
            //    Description = "Maya Hotel is located in the center of the historical, tourist, shopping and entertainment district of Laleli. Located in the center of Istanbul, guests can visit the Great Bazaar, Blue Mosque, Hagia Sophia, located just a 10-minute walk from the hotel. In addition, the hotel offers numerous restaurants, shopping and entertainment centers of the city. Vezneciler Metro Station, which provides easy access to other city attractions, is within walking distance.",
            //    StarsCount = 2
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Sun Comfort Hotel",
            //    Description = "Sun Comfort Hotel is located in the center of the Kumkapi quarter of Fatih, about 1.5 km from historical sites such as the Blue Mosque, Hagia Sophia and Topkapi Sultan's Palace. Fatih is a great choice for travelers interested in monuments, architecture and the old city.",
            //    StarsCount = 3
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Cihangir Hotel",
            //    Description = "Cihangir Hotel is located in Istanbul overlooking the Bosphorus. The hotel was completely renovated in 2013. The lively Taksim district with the nearest metro station is 300 meters away.",
            //    StarsCount = 3
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Grand Mark Hotel",
            //    Description = "The hotel is located in the Fatih district,just a kilometer from the center of Istanbul.From here it is easy to get to all the main attractions of the city.",
            //    StarsCount = 4
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Grand Liza Hotel",
            //    Description = "The comfortable 4-star hotel is located in the Kumkapi district, close to a palace, a basilica and a church.",
            //    StarsCount = 4
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Sed Hotel",
            //    Description = "Hotel Sed is 300 meters from the Kabatas Tram Stop, which provides easy access to the old town. It offers air-conditioned rooms with a flat-screen TV, free Wi-Fi and a terrace with panoramic sea views.",
            //    StarsCount = 4
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Abel Hotel",
            //    Description = "The hotel was opened in 2012,consists of one 7 - storey building.Located in the lively district of Istanbul,right next to the oriental bazaar, shops and 1.8 km from the Blue Mosque.",
            //    StarsCount = 5
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Raimond Hotel",
            //    Description = "Raimond Hotel is located in a quiet area, just a 10-minute walk from the Blue Mosque. Raimond Hotel has welcomed guests in Istanbul since 2018. Fatih is a great choice if you are interested in the old city, architecture and history.",
            //    StarsCount = 5
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "Alphonse",
            //    Description = "Alphonse Hotel is located in the Kumkapi area, which is popular for its many seafood restaurants. Fatih is a great choice if you are interested in the old city, architecture and monuments.",
            //    StarsCount = 5
            //});
            //db.Hotels.Add(new Hotel
            //{
            //    Title = "The Ottoman City Hotel ",
            //    Description = "Ottoman City Hotel is located in Istanbul.Nearby are the Onur Süper Market and the supermarket.Majidiyokoy Square is a 9 - minute drive away and Florence Nightingale Sisli Hospital is a 14 - minute drive away.Suitable for families.",
            //    StarsCount = 5
            //});

            //var turkey = new Country { Title = "Turkey" };
            //db.Countries.Add(turkey);
            //var ukraine = new Country { Title = "Ukraine" };
            //db.Countries.Add(ukraine);
            //var egypt = new Country { Title = "Egypt" };
            //db.Countries.Add(egypt);
            //var argentina = new Country { Title = "Argentina" };
            //db.Countries.Add(egypt);


            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = turkey,
            //    Price = 245,
            //    MinPeopleCount = 1,
            //    MaxPeopleCount = 5,
            //    StartDate = DateTime.ParseExact("28/03/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("07/04/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    CountOfTour=5,
            //    HotTour = true,
            //    Type = TourType1,
            //    FeedingType = FeedingType1,
            //    Hotel = defaultHotel
            //});
            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = ukraine,
            //    Price = 220,
            //    MinPeopleCount = 2,
            //    MaxPeopleCount = 8,
            //    StartDate = DateTime.ParseExact("01/03/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("04/03/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    HotTour = true,
            //    Type = TourType2,
            //    FeedingType = FeedingType2,
            //    Hotel = Hotel2
            //});
            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = egypt,
            //    Price = 250,
            //    MinPeopleCount = 2,
            //    MaxPeopleCount = 6,
            //    StartDate = DateTime.ParseExact("03/04/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("15/04/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    CountOfTour = 6,
            //    HotTour = true,
            //    Type = TourType1,
            //    FeedingType = FeedingType3,
            //    Hotel = defaultHotel
            //});
            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = turkey,
            //    Price = 400,
            //    MinPeopleCount = 2,
            //    MaxPeopleCount = 6,
            //    StartDate = DateTime.ParseExact("10/05/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("22/05/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    HotTour = true,
            //    Type = TourType3,
            //    FeedingType = FeedingType4,
            //    Hotel = Hotel2
            //});
            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = egypt,
            //    Price = 120,
            //    MinPeopleCount = 2,
            //    MaxPeopleCount = 6,
            //    StartDate = DateTime.ParseExact("06/05/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("12/05/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    HotTour = false,
            //    Type = TourType1,
            //    FeedingType = FeedingType5,
            //    Hotel = Hotel2
            //});

            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = ukraine,
            //    Price = 155,
            //    MinPeopleCount = 1,
            //    MaxPeopleCount = 8,
            //    StartDate = DateTime.ParseExact("03/03/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("05/03/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    HotTour = true,
            //    Type = TourType2,
            //    FeedingType = FeedingType6,
            //    Hotel = defaultHotel
            //});

            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = argentina,
            //    Price = 85,
            //    MinPeopleCount = 1,
            //    MaxPeopleCount = 7,
            //    StartDate = DateTime.ParseExact("12/05/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("24/05/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    HotTour = false,
            //    Type = TourType2,
            //    FeedingType = FeedingType4,
            //    Hotel = Hotel2
            //});

            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = turkey,
            //    Price = 170,
            //    MinPeopleCount = 2,
            //    MaxPeopleCount = 7,
            //    StartDate = DateTime.ParseExact("28/04/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("07/05/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    HotTour = false,
            //    Type = TourType3,
            //    FeedingType = FeedingType5,
            //    Hotel = defaultHotel
            //});

            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = argentina,
            //    Price = 150,
            //    MinPeopleCount = 1,
            //    MaxPeopleCount = 15,
            //    StartDate = DateTime.ParseExact("14/03/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("28/03/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    HotTour = false,
            //    Type = TourType2,
            //    FeedingType = FeedingType4,
            //    Hotel = Hotel2
            //});

            //db.Tours.Add(new Tour
            //{
            //    Title = "Reydel Hotel",
            //    Description = "Decorated in soft colors, the stylish rooms at Reydel Hotel include a TV, a safe and a private bathroom with shower and hairdryer.",
            //    Country = turkey,
            //    Price = 150,
            //    MinPeopleCount = 1,
            //    MaxPeopleCount = 7,
            //    StartDate = DateTime.ParseExact("28/02/2020", "dd/MM/yyyy", null),
            //    EndDate = DateTime.ParseExact("07/03/2020", "dd/MM/yyyy", null),
            //    // ImageLink = "/Content/images/time-to-travel.jpg",
            //    HotTour = true,
            //    Type = TourType1,
            //    FeedingType = FeedingType2,
            //    Hotel = defaultHotel
            //});

            db.FeedingTypes.Add(new FeedingType { Title = "OB" });
            db.FeedingTypes.Add(new FeedingType { Title = "BB" });
            db.FeedingTypes.Add(new FeedingType { Title = "HB" });
            db.FeedingTypes.Add(new FeedingType { Title = "FB" });
            db.FeedingTypes.Add(new FeedingType { Title = "AI" });
            db.FeedingTypes.Add(new FeedingType { Title = "UAI" });


            db.OrderStatuses.Add(new OrderStatus { Title = "Registered" });
            db.OrderStatuses.Add(new OrderStatus { Title = "Not registered" });
            db.OrderStatuses.Add(new OrderStatus { Title = "Paid" });
            db.OrderStatuses.Add(new OrderStatus { Title = "Canceled" });

            db.TourTypes.Add(new TourType { Title = "Sea holiday" });
            db.TourTypes.Add(new TourType { Title = "Bus tour" });
            db.TourTypes.Add(new TourType { Title = "Shopping" });

            db.Hotels.Add(new Hotel
            {
                Title = "Pera Tulip Hotel",
                Description = "This design hotel is located in the Beyoglu district. Tulip By Molton Hotels is just a few steps from Sishane MRT Station, Nostalgic and Tunnel Tram Stations, providing easy access to Sultanahmet and the rest of Istanbul. Tulip By Molton Hotels has been welcoming guests to its comfortable rooms since 2008. The hotel is perfect for both sightseeing and business trips.",
                StarsCount = 1
            });

            db.Hotels.Add(new Hotel
            {
                Title = "Ferman Apart Hotel",
                Description = "The aparthotel is located in Istanbul, 800 meters from the Congress Center, 1.9 km from Taksim Square, 2.5 km from Istiklal Street and 2.5 km from Dolmabahce Palace. Dolmabahce Clock Tower is 3 km from the hotel, while Galata Tower is 4.4 km away. Suitable for business tours, romantic getaways.",
                StarsCount = 1
            });

            db.Hotels.Add(new Hotel
            {
                Title = "Arts Hotel",
                Description = "Arts Istanbul Design Hotel is 350 meters from Osmanbey Metro Station and a 10-minute walk from Taksim Square and the fashionable Nishantashi district. Perfect for both sightseeing and business trips.",
                StarsCount = 1
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Florya Park Hotel",
                Description = "Florya Park Hotel is located near Ataturk International Airport and Istanbul Exhibition Center.Bakırköy is a great choice for travelers interested in food, culture and local cuisine.",
                StarsCount = 2
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Maya Hotel",
                Description = "Maya Hotel is located in the center of the historical, tourist, shopping and entertainment district of Laleli. Located in the center of Istanbul, guests can visit the Great Bazaar, Blue Mosque, Hagia Sophia, located just a 10-minute walk from the hotel. In addition, the hotel offers numerous restaurants, shopping and entertainment centers of the city. Vezneciler Metro Station, which provides easy access to other city attractions, is within walking distance.",
                StarsCount = 2
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Sun Comfort Hotel",
                Description = "Sun Comfort Hotel is located in the center of the Kumkapi quarter of Fatih, about 1.5 km from historical sites such as the Blue Mosque, Hagia Sophia and Topkapi Sultan's Palace. Fatih is a great choice for travelers interested in monuments, architecture and the old city.",
                StarsCount = 3
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Cihangir Hotel",
                Description = "Cihangir Hotel is located in Istanbul overlooking the Bosphorus. The hotel was completely renovated in 2013. The lively Taksim district with the nearest metro station is 300 meters away.",
                StarsCount = 3
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Grand Mark Hotel",
                Description = "The hotel is located in the Fatih district,just a kilometer from the center of Istanbul.From here it is easy to get to all the main attractions of the city.",
                StarsCount = 4
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Grand Liza Hotel",
                Description = "The comfortable 4-star hotel is located in the Kumkapi district, close to a palace, a basilica and a church.",
                StarsCount = 4
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Sed Hotel",
                Description = "Hotel Sed is 300 meters from the Kabatas Tram Stop, which provides easy access to the old town. It offers air-conditioned rooms with a flat-screen TV, free Wi-Fi and a terrace with panoramic sea views.",
                StarsCount = 4
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Abel Hotel",
                Description = "The hotel was opened in 2012,consists of one 7 - storey building.Located in the lively district of Istanbul,right next to the oriental bazaar, shops and 1.8 km from the Blue Mosque.",
                StarsCount = 5
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Raimond Hotel",
                Description = "Raimond Hotel is located in a quiet area, just a 10-minute walk from the Blue Mosque. Raimond Hotel has welcomed guests in Istanbul since 2018. Fatih is a great choice if you are interested in the old city, architecture and history.",
                StarsCount = 5
            });
            db.Hotels.Add(new Hotel
            {
                Title = "Alphonse",
                Description = "Alphonse Hotel is located in the Kumkapi area, which is popular for its many seafood restaurants. Fatih is a great choice if you are interested in the old city, architecture and monuments.",
                StarsCount = 5
            });
            db.Hotels.Add(new Hotel
            {
                Title = "The Ottoman City Hotel ",
                Description = "Ottoman City Hotel is located in Istanbul.Nearby are the Onur Süper Market and the supermarket.Majidiyokoy Square is a 9 - minute drive away and Florence Nightingale Sisli Hospital is a 14 - minute drive away.Suitable for families.",
                StarsCount = 5
            });

            db.Countries.Add(new Country { Title = "Turkey" });
            db.Countries.Add(new Country { Title = "Ukraine" });
            db.Countries.Add(new Country { Title = "Egypt" });
            db.Countries.Add(new Country { Title = "Argentina" });

            db.SaleSettings.Add(new SaleSettings { MaxSale=25,SaleStep=5 });

            base.Seed(db);
        }
    }
}
