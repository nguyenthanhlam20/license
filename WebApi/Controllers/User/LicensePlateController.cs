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
using Repositories.Districts;
using Repositories.Series;
using System.Collections.Generic;

namespace WebApi.Controllers.User
{
    [Authorize(Roles = "User")]
    [Route("api/User/[controller]/[action]")]
    [ApiController]
    public class LicensePlateController : ControllerBase
    {
        private readonly ILicensePlateRepository _repository;
        private readonly IDistrictRepository _districtRepository;
        private readonly ISeriRepository _seriRepository;
        private readonly IMapper _mapper;

        public LicensePlateController(ILicensePlateRepository repository,
            IDistrictRepository districtRepository,
            ISeriRepository seriRepository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _districtRepository = districtRepository;
            _seriRepository = seriRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLicensePlates([FromQuery] string email)
        {
            List<LicensePlate> licensePlates = await _repository.GetLicensePlates(email);

            List<LicensePlateVM> vms = _mapper.Map<List<LicensePlateVM>>(licensePlates);
            return Ok(vms);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrent([FromQuery] string email)
        {

            List<LicensePlate> licensePlates = await _repository.GetLicensePlates(email);

            LicensePlateVM licensePlateVM = _mapper.Map<LicensePlateVM>(licensePlates.LastOrDefault());
            return Ok(licensePlateVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddLicensePlate([FromBody] LicensePlateVM licensePlateVM)
        {
            try
            {
                var licensePlate = _mapper.Map<LicensePlate>(licensePlateVM);
                int number = await _repository.GetAvailableNumber(licensePlate);
                licensePlate.Number = number;

                District district = await _districtRepository.GetDistrictById(licensePlate.DistrictId);
                Seri seri = await _seriRepository.GetSeriById(licensePlate.SeriesId);

                licensePlate.LicensePlateNumber = $"{district.Prefix}{seri.Title} - {number / 100}.{number % 100}";
                Console.WriteLine(licensePlate.LicensePlateNumber);

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
