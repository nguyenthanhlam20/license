using AutoMapper;
using DataAccess.Models;
using ViewModels.Accounts;

namespace WebApi.Config
{
    public class AutoMapperConfig : Profile
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                // Mapper Account
                config.CreateMap<AccountVM, Account>()
                        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
                config.CreateMap<Account, AccountVM>();

            });

            return mapperConfig.CreateMapper();
        }
    }
}
