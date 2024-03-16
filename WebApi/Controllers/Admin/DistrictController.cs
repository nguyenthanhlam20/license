using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Districts;
using ViewModels;
using ViewModels.Districts;
using ViewModels.Paging;

namespace WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/Admin/[controller]/[action]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private IDistrictRepository _repository;
        private readonly IMapper _mapper;

        public DistrictController(IDistrictRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> GetDistricts(DistrictPagingRequest request)
        {
            try
            {
                DistrictPagingRequest response = await _repository
                    .GetDistricts(request);

                if (response != null)
                {
                    response.ItemVMs = _mapper.Map<List<DistrictVM>>(response.Items);
                    response.Items = null;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetDistrictById([FromQuery] int districtId)
        {
            try
            {
                District district = await _repository.GetDistrictById(districtId);

                if (district == null)
                {
                    return NotFound();
                }

                DistrictVM districtVM = _mapper.Map<DistrictVM>(district);
                return Ok(districtVM);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDistrict([FromBody] DistrictVM districtVM)
        {
            try
            {
                var district = _mapper.Map<District>(districtVM);

                bool status = await _repository.UpdateDistrict(district);
                if (!status)
                {
                    throw new Exception("Cập nhật tỉnh thất bại.");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Cập nhật tỉnh thành công." });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDistrict([FromBody] DistrictVM districtVM)
        {
            try
            {
                var district = _mapper.Map<District>(districtVM);
                bool status = await _repository.AddDistrict(district);
                if (!status)
                {
                    throw new Exception("Thêm mới tỉnh thất bại.");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Thêm mới tỉnh thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }
    }
}
