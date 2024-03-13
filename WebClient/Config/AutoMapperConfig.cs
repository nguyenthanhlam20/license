using AutoMapper;
using ViewModels.Accounts;

namespace WebClient.Config
{
    public class AutoMapperConfig : Profile
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {

            });

            return mapperConfig.CreateMapper();
        }
    }
}
