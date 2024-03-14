using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.LicensePlates;
using ViewModels.LicensePlates;
using ViewModels;

namespace WebApi.Controllers.Shared
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/Shared/[controller]/[action]")]
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
        public async Task<IActionResult> AddLicensePlate([FromBody] LicensePlateVM licensePlateVM)
        {
            try
            {
                if (licensePlateVM == null)
                {
                    throw new Exception("Didn't recieved licensePlate information");
                }
                // check exist campus
                var licensePlate = _mapper.Map<LicensePlate>(licensePlateVM);
                bool isExisted = await _repository.IsExistedLicensePlate(licensePlateVM.LicensePlateId ?? 0, licensePlateVM.LicensePlateNumber);
                if (isExisted)
                {
                    throw new Exception($"LicensePlate with the name '{licensePlateVM.LicensePlateNumber}' existed!");
                }

                //add new campus
                bool status = await _repository.AddLicensePlate(licensePlate);
                if (!status)
                {
                    throw new Exception("Add licensePlate failed!");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Add licensePlate successful!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }
    }
}
