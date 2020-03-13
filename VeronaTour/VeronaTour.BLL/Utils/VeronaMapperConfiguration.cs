using AutoMapper;
using VeronaTour.BLL.DTOs;
using VeronaTour.DAL.Entites;

namespace VeronaTour.BLL.Utils
{
    public class VeronaMapperConfiguration
    {
        public static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TourType, TourTypeDTO>();
                cfg.CreateMap<TourTypeDTO, TourType>();

                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelDTO, Hotel>();

                cfg.CreateMap<UserDTO, User>()
                    .ForMember(dest => dest.Role, opt => opt.Ignore());
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.UserId))
                    .ForMember(dest => dest.Role, opt => opt.Ignore());

                cfg.CreateMap<FeedingType, FeedingTypeDTO>();
                cfg.CreateMap<FeedingTypeDTO, FeedingType>();

                cfg.CreateMap<Tour, TourDTO>();
                cfg.CreateMap<TourDTO, Tour>();

                cfg.CreateMap<UserRole, UserRoleDTO>();

                cfg.CreateMap<OrderStatus, OrderStatusDTO>();
                cfg.CreateMap<Order, OrderDTO>();

                cfg.CreateMap<Country, CountryDTO>();
                cfg.CreateMap<CountryDTO, Country>();

                cfg.CreateMap<SaleSettingsDTO, SaleSettings>();
                cfg.CreateMap<SaleSettings, SaleSettingsDTO>();
            });
        }
    }
}