using DataAccess.LicensePlateContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels.Paging;

namespace Repositories.Series
{
    public class SeriRepository : ISeriRepository
    {
        private readonly LicensePlateDbContext _context;
        public SeriRepository(LicensePlateDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddSeri(Seri seri)
        {
            try
            {
                await _context.Series.AddAsync(seri);

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

        public async Task<Seri> GetSeriById(int id)
        {
            Seri? seri = new();
            try
            {
                seri = await _context.Series.SingleOrDefaultAsync(s => s.SeriId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return seri;
        }

        public async Task<SeriPagingRequest> GetSeries(SeriPagingRequest request)
        {
            try
            {
                var query = await _context.Series.ToListAsync();
                if (!String.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(c => c.Title.ToLower().Contains(request.SearchTerm)).ToList();
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

        public async Task<List<Seri>> GetSeries()
        {
            try
            {
                return await _context.Series.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return new List<Seri>();
        }

        public async Task<bool> IsExistedSeri(int seriId, string seriName)
        {
            try
            {
                Seri? seri = await _context.Series.SingleOrDefaultAsync(s => s.Title.ToLower().Equals(seriName.ToLower()));
                if (seri != null && seri.SeriId != seriId) return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return true;
            }
        }

        public async Task<bool> UpdateSeri(Seri seri)
        {
            try
            {
                var existingSeri = await _context.Series.SingleOrDefaultAsync(c => c.SeriId == seri.SeriId);

                if (existingSeri != null)
                {
                    existingSeri.Title = seri.Title;
                    existingSeri.Description = seri.Description;

                    _context.Update(existingSeri);
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
