using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Paging;
using ViewModels.Districts;
using Repositories.Districts;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class DistrictController : ControllerBase
    {
        private IDistrictRepository _repository;
        private readonly IMapper _mapper;

        public DistrictController(IDistrictRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetDistricts()
        {
            try
            {
                var subjects = await _repository.GetDistricts();
                List<DistrictVM> subjectVMs = new();
                if (subjects != null)
                {
                    subjectVMs = _mapper.Map<List<DistrictVM>>(subjects);
                }
                return Ok(subjectVMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDistrictById(int subjectId)
        {
            try
            {
                var subject = await _repository.GetDistrictById(subjectId);
                if (subject != null)
                {
                    DistrictVM subjectVM = _mapper.Map<DistrictVM>(subject);
                    return Ok(subjectVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
            return NotFound();
        }
    }
}
