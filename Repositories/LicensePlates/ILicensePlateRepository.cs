using DataAccess.Models;
using ViewModels.Paging;

namespace Repositories.LicensePlates
{
    public interface ILicensePlateRepository
    {
        Task<LicensePlatePagingRequest> GetLicensePlates(LicensePlatePagingRequest request);
        Task<List<LicensePlate>> GetLicensePlates(string email);
        Task<LicensePlate> GetLicensePlateById(int id);
        Task<bool> AddLicensePlate(LicensePlate licensePlate);
        Task<bool> UpdateLicensePlate(LicensePlate licensePlate);
        Task<bool> IsExistedLicensePlate(int licensePlateId, string licensePlateName);
    }
}
