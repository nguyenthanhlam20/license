using DataAccess.LicensePlateContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using ViewModels.Paging;

namespace Repositories.Districts
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly LicensePlateDbContext _context;
        public DistrictRepository(LicensePlateDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDistrict(District district)
        {
            try
            {
                await _context.Districts.AddAsync(district);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return false;
        }

        public async Task<District> GetDistrictById(int id)
        {
            District? district = new();
            try
            {
                district = await _context.Districts
                    .Include(s => s.LicensePlates)
                    .SingleOrDefaultAsync(s => s.DistrictId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return district;
        }

        public async Task<DistrictPagingRequest> GetDistricts(DistrictPagingRequest request)
        {
            try
            {
                var query = await _context.Districts.Include(x => x.LicensePlates).ToListAsync();
                if (!String.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(c => c.Name.ToLower().Contains(request.SearchTerm)
                    || c.Prefix.ToLower().Contains(request.SearchTerm)).ToList();
                }

                //Set totoal pages for paging
                request.TotalRecord = query.Count();
                request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
                query = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();

                request.Items = query;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return request;
        }

        public async Task<List<District>> GetDistricts()
        {
            try
            {
                return await _context.Districts.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return new List<District>();
        }

        public async Task<bool> IsExistedDistrict(int districtId, string districtName)
        {
            try
            {
                District? district = await _context.Districts.SingleOrDefaultAsync(s => s.Name.ToLower().Equals(districtName.ToLower()));
                if (district != null && district.DistrictId != districtId) return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return true;
            }
        }

        public async Task<bool> UpdateDistrict(District district)
        {
            try
            {
                var existingDistrict = await _context.Districts.SingleOrDefaultAsync(c => c.DistrictId == district.DistrictId);

                if (existingDistrict != null)
                {
                    existingDistrict.Name = district.Name;
                    existingDistrict.Prefix = district.Prefix;

                    _context.Update(existingDistrict);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return false;
        }
    }
}