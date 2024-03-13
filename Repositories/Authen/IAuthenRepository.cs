using ViewModels.Authens;
using ViewModels;

namespace Repositories.Authen
{
    public interface IAuthenRepository
    {
        Task<ResponseVM> AccessByEmail(SignInVM request);
        Task<bool> IsAccountExisted(string email);
    }
}
