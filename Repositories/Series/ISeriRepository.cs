using DataAccess.Models;
using ViewModels.Paging;

namespace Repositories.Series
{
    public interface ISeriRepository
    {
        Task<SeriPagingRequest> GetSeries(SeriPagingRequest request);
        Task<List<Seri>> GetSeries();
        Task<Seri> GetSeriById(int id);
        Task<bool> AddSeri(Seri seri);
        Task<bool> UpdateSeri(Seri seri);
        Task<bool> IsExistedSeri(int seriId, string seriName);
    }
}
