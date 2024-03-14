using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Accounts;
using Repositories.LicensePlates;
using ViewModels.Accounts;
using WebApi.Services;
using DataAccess.Models;

namespace WebApi.Controllers.User
{
    [Authorize(Roles = "User")]
    [Route("api/User/[controller]/[action]")]
    [ApiController]
    public class LicensePlateController : ControllerBase
    {
        private readonly ILicensePlateRepository _repository;
        private readonly IMapper _mapper;

        public LicensePlateController(ILicensePlateRepository repository, IMapper mapper )
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
    }
}
