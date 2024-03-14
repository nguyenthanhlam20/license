using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Districts;

namespace ViewModels.Paging
{
    public class DistrictPagingRequest : PagingRequestBase<District>
    {
        public List<DistrictVM>? ItemVMs { get; set; }

    }
}
