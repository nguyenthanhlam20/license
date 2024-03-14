using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Paging;

namespace Repositories.Districts
{
    public interface IDistrictRepository
    {
        Task<DistrictPagingRequest> GetDistricts(DistrictPagingRequest request);
        Task<List<District>> GetDistricts();
        Task<District> GetDistrictById(int id);
        Task<bool> AddDistrict(District district);
        Task<bool> UpdateDistrict(District district);
        Task<bool> IsExistedDistrict(int districtId, string districtName);
    }
}
