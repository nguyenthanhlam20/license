using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Series;
using DataAccess.Models;
using ViewModels.Series;

namespace WebApi.Controllers.Shared
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/Shared/[controller]/[action]")]
    [ApiController]
    public class SeriController : ControllerBase
    {
        private ISeriRepository _repository;
        private readonly IMapper _mapper;

        public SeriController(ISeriRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetSeries()
        {
            try
            {
                List<Seri> response = await _repository.GetSeries();

                List<SeriVM> result = _mapper.Map<List<SeriVM>>(response);
               
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return NotFound();
            }
        }
    }
}
