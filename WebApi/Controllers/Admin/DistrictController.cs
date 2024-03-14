using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.Districts;
using ViewModels.Paging;
using Repositories.Districts;

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
                if (districtVM == null)
                {
                    throw new Exception("Didn't recieved district information");
                }

                bool isExisted = await _repository.IsExistedDistrict(districtVM.DistrictId ?? 0, districtVM.Name);
                if (isExisted)
                {
                    throw new Exception($"District with the name '{districtVM.Name}' existed!");
                }

                var district = _mapper.Map<District>(districtVM);

                bool status = await _repository.UpdateDistrict(district);
                if (!status)
                {
                    throw new Exception("Update district failed!");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Update district successful!" });
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
                if (districtVM == null)
                {
                    throw new Exception("Didn't recieved district information");
                }
                // check exist campus
                var district = _mapper.Map<District>(districtVM);
                bool isExisted = await _repository.IsExistedDistrict(districtVM.DistrictId ?? 0, districtVM.Name);
                if (isExisted)
                {
                    throw new Exception($"District with the name '{districtVM.Name}' existed!");
                }

                //add new campus
                bool status = await _repository.AddDistrict(district);
                if (!status)
                {
                    throw new Exception("Add district failed!");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Add district successful!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }
    }
}
