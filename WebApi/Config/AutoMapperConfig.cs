using AutoMapper;
using DataAccess.Models;
using ViewModels.Accounts;
using ViewModels.Districts;
using ViewModels.LicensePlates;
using ViewModels.Series;

namespace WebApi.Config
{
    public class AutoMapperConfig : Profile
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                // Mapper Account
                config.CreateMap<AccountVM, Account>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
                config.CreateMap<Account, AccountVM>();

                // Mapper District
                config.CreateMap<District, DistrictVM>();
                config.CreateMap<DistrictVM, District>();

                // Mapper Seri
                config.CreateMap<Seri, SeriVM>();
                config.CreateMap<SeriVM, Seri>();

                // Mapper License Plate
                config.CreateMap<LicensePlate, LicensePlateVM>();
                config.CreateMap<LicensePlateVM, LicensePlate>();
            });

            return mapperConfig.CreateMapper();
        }
    }
}
