using DataAccess.Models;
using ViewModels.Series;

namespace ViewModels.Paging
{
    public class SeriPagingRequest : PagingRequestBase<Seri>
    {
        public List<SeriVM>? ItemVMs { get; set; }
    }
}
