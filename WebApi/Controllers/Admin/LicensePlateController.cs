using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.LicensePlates;
using ViewModels.Paging;
using Repositories.LicensePlates;

namespace WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/Admin/[controller]/[action]")]
    [ApiController]
    public class LicensePlateController : ControllerBase
    {
        private ILicensePlateRepository _repository;
        private readonly IMapper _mapper;

        public LicensePlateController(ILicensePlateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> GetLicensePlates(LicensePlatePagingRequest request)
        {
            try
            {
                LicensePlatePagingRequest response = await _repository
                    .GetLicensePlates(request);

                if (response != null)
                {
                    response.ItemVMs = _mapper.Map<List<LicensePlateVM>>(response.Items);
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
        public async Task<IActionResult> GetLicensePlateById([FromQuery] int licensePlateId)
        {
            try
            {
                LicensePlate licensePlate = await _repository.GetLicensePlateById(licensePlateId);

                if (licensePlate == null)
                {
                    return NotFound();
                }

                LicensePlateVM licensePlateVM = _mapper.Map<LicensePlateVM>(licensePlate);
                return Ok(licensePlateVM);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLicensePlate([FromBody] LicensePlateVM licensePlateVM)
        {
            try
            {
                if (licensePlateVM == null)
                {
                    throw new Exception("Didn't recieved licensePlate information");
                }

                bool isExisted = await _repository.IsExistedLicensePlate(licensePlateVM.LicensePlateId ?? 0, licensePlateVM.LicensePlateNumber);
                if (isExisted)
                {
                    throw new Exception($"LicensePlate with the name '{licensePlateVM.LicensePlateNumber}' existed!");
                }

                var licensePlate = _mapper.Map<LicensePlate>(licensePlateVM);

                bool status = await _repository.UpdateLicensePlate(licensePlate);
                if (!status)
                {
                    throw new Exception("Update licensePlate failed!");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Update licensePlate successful!" });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }

      
    }
}
