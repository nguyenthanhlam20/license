using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.LicensePlates;
using ViewModels.LicensePlates;
using ViewModels.Paging;

namespace WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/Admin/[controller]/[action]")]
    [ApiController]
    public class LicensePlateController : ControllerBase
    {
        private ILicensePlateRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILicensePlateRepository _licensePlateRepository;

        public LicensePlateController(ILicensePlateRepository repository,
                        ILicensePlateRepository licensePlateRepository,

            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _licensePlateRepository = licensePlateRepository;

        }


        [HttpGet]
        public async Task<IActionResult> GetLicensePlates([FromQuery] string email)
        {
            List<LicensePlate> licensePlates = await _licensePlateRepository.GetLicensePlates(email);

            List<LicensePlateVM> vms = _mapper.Map<List<LicensePlateVM>>(licensePlates);
            return Ok(vms);
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
    }
}
