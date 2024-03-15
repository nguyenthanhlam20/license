using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Series;
using ViewModels.Series;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class SeriController  : ControllerBase
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
                return Ok(_mapper.Map<List<SeriVM>>(response));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return NotFound();
            }
        }
    }
}
