using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Districts;
using ViewModels.LicensePlates;

namespace ViewModels.Paging
{
    public class LicensePlatePagingRequest : PagingRequestBase<LicensePlate>
    {
        public List<LicensePlateVM>? ItemVMs { get; set; }
    }
}
