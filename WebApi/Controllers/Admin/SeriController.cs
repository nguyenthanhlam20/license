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
                var seri = _mapper.Map<Seri>(seriVM);

                bool status = await _repository.UpdateSeri(seri);
                if (!status)
                {
                    throw new Exception("Cập nhật số seri thất bại.");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Cập nhật số seri thành công." });
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
                var seri = _mapper.Map<Seri>(seriVM);
             
                bool status = await _repository.AddSeri(seri);
                if (!status)
                {
                    throw new Exception("Thêm số seri thất bại.");
                }

                return Ok(new ResponseVM() { Status = true, Message = "Thêm số seri thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }
    }
}
