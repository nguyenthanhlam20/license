using DataAccess.LicensePlateContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using ViewModels.Paging;

namespace Repositories.LicensePlates
{
    public class LicensePlateRepository : ILicensePlateRepository
    {
        private readonly LicensePlateDbContext _context;
        public LicensePlateRepository(LicensePlateDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddLicensePlate(LicensePlate licensePlate)
        {
            try
            {
                await _context.LicensePlates.AddAsync(licensePlate);

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

        public async Task<LicensePlate> GetLicensePlateById(int id)
        {
            LicensePlate? licensePlate = new();
            try
            {
                licensePlate = await _context.LicensePlates
                    .SingleOrDefaultAsync(s => s.LicensePlateId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return licensePlate;
        }

        public async Task<LicensePlatePagingRequest> GetLicensePlates(LicensePlatePagingRequest request)
        {
            try
            {
                var query = await _context.LicensePlates.ToListAsync();
                if (!String.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(c => c.LicensePlateNumber.ToLower().Contains(request.SearchTerm)).ToList();
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

        public async Task<List<LicensePlate>> GetLicensePlates(string email)
        {
            try
            {
                return await _context.LicensePlates.Include(x => x.Account).Where(x => x.Account.Email.Equals(email)).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return new List<LicensePlate>();
        }

        public async Task<bool> IsExistedLicensePlate(int licensePlateId, string licensePlateName)
        {
            try
            {
                LicensePlate? licensePlate = await _context.LicensePlates.SingleOrDefaultAsync(s => s.LicensePlateNumber.ToLower().Equals(licensePlateName.ToLower()));
                if (licensePlate != null && licensePlate.LicensePlateId != licensePlateId) return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return true;
            }
        }

        public async Task<bool> UpdateLicensePlate(LicensePlate licensePlate)
        {
            try
            {
                var existingLicensePlate = await _context.LicensePlates.SingleOrDefaultAsync(c => c.LicensePlateId == licensePlate.LicensePlateId);

                if (existingLicensePlate != null)
                {
                    existingLicensePlate.LicensePlateNumber = licensePlate.LicensePlateNumber;

                    _context.Update(existingLicensePlate);
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