using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Paging;
using ViewModels.Districts;
using Repositories.Districts;
using System.Collections.Generic;
using DataAccess.Models;

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
        public async Task<IActionResult> GetAllDistricts()
        {
            try
            {
                List<District> response = await _repository.GetDistricts();
                return Ok(_mapper.Map<List<DistrictVM>>(response));
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
