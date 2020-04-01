using AutoMapper;
using VeronaTour.BLL.DTOs;
using VeronaTour.DAL.Entites;

namespace VeronaTour.BLL.Utils
{
    /// <summary>
    ///     Provides configuration for mapping BLL`s classes
    /// </summary>
    public class VeronaMapperConfiguration
    {
        /// <summary>
        ///     Get configuratino for mapping
        /// </summary>
        /// <returns>Mapping configuration</returns>
        public static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TourType, TourTypeDTO>();
                cfg.CreateMap<TourTypeDTO, TourType>();

                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelDTO, Hotel>();

                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.UserId))
                    .ForMember(dest => dest.Role, opt => opt.Ignore());

                cfg.CreateMap<FeedingType, FeedingTypeDTO>();
                cfg.CreateMap<FeedingTypeDTO, FeedingType>();

                cfg.CreateMap<Tour, TourDTO>();
                cfg.CreateMap<TourDTO, Tour>();

                cfg.CreateMap<OrderStatus, OrderStatusDTO>();
                cfg.CreateMap<Order, OrderDTO>();

                cfg.CreateMap<Country, CountryDTO>();
                cfg.CreateMap<CountryDTO, Country>();

                cfg.CreateMap<SaleSettingsDTO, SaleSettings>();
                cfg.CreateMap<SaleSettings, SaleSettingsDTO>();

                cfg.CreateMap<ExceptionDetail, ExceptionDetailDTO>();
            });
        }
    }
}