using ViewModels.Paging;
using ViewModels.Accounts;
using DataAccess.Models;
using ViewModels;

namespace Repositories.Accounts
{
    public interface IAccountRepository
    {
        Task<AccountPagingRequest> GetAll(AccountPagingRequest request);
        Task<AccountVM> GetAccountByEmail(string email);
        Task<ResponseVM> CreateAccountManualAsync(Account request, List<string> roles);
        Task<ResponseVM> UpdateAccountManualAsync(Account request);
        Task<Guid> GetUserId(string email);
    }
}
