using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.Series;
using ViewModels.Paging;
using Repositories.Series;

namespace WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/Admin/[controller]/[action]")]
    [ApiController]
    public class SeriController : ControllerBase
    {
        private ISeriRepository _repository;
        private readonly IMapper _mapper;

        public SeriController(ISeriRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> GetSeries(SeriPagingRequest request)
        {
            try
            {
                SeriPagingRequest response = await _repository
                    .GetSeries(request);

                if (response != null)
                {
                    response.ItemVMs = _mapper.Map<List<SeriVM>>(response.Items);
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
        public async Task<IActionResult> GetSeriById([FromQuery] int seriId)
        {
            try
            {
                Seri seri = await _repository.GetSeriById(seriId);

                if (seri == null)
                {
                    return NotFound();
                }

                SeriVM seriVM = _mapper.Map<SeriVM>(seri);
                return Ok(seriVM);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSeri([FromBody] SeriVM seriVM)
        {
            try
            {
                if (seriVM == null)
                {
                    throw new Exception("Didn't recieved seri information");
                }

                var seri = _mapper.Map<Seri>(seriVM);

                bool status = await _repository.UpdateSeri(seri);
                if (!status)
                {
                    throw new Exception("Update seri failed!");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Update seri successful!" });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSeri([FromBody] SeriVM seriVM)
        {
            try
            {
                if (seriVM == null)
                {
                    throw new Exception("Didn't recieved seri information");
                }
                // check exist campus
                var seri = _mapper.Map<Seri>(seriVM);
             
                bool status = await _repository.AddSeri(seri);
                if (!status)
                {
                    throw new Exception("Add seri failed!");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Add seri successful!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }
    }
}
