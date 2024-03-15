using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Accounts;
using Repositories.LicensePlates;
using ViewModels.Accounts;
using WebApi.Services;
using DataAccess.Models;
using ViewModels.LicensePlates;
using ViewModels;

namespace WebApi.Controllers.User
{
    [Authorize(Roles = "User")]
    [Route("api/User/[controller]/[action]")]
    [ApiController]
    public class LicensePlateController : ControllerBase
    {
        private readonly ILicensePlateRepository _repository;
        private readonly IMapper _mapper;

        public LicensePlateController(ILicensePlateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetLicensePlates([FromQuery] string email)
        {
            List<LicensePlate> account = await _repository.GetLicensePlates(email);
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicensePlate([FromBody] LicensePlateVM licensePlateVM)
        {
            try
            {
                var licensePlate = _mapper.Map<LicensePlate>(licensePlateVM);
                licensePlate.Number = await _repository.GetAvailableNumber(licensePlate);

                bool status = await _repository.AddLicensePlate(licensePlate);
                if (!status)
                {
                    throw new Exception("Bấm biển số thất bại");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Bấm biển số thành công" });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }

    }
}
