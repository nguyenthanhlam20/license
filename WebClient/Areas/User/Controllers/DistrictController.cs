using Core.Constants;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Paging;
using ViewModels.Districts;
using WebClient.Helpers;
using WebClient.Services;

namespace WebClient.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[Controller]/[Action]")]
    [Authorize(Roles = "User")]
    public class DistrictController : Controller
    {
        private readonly ClientService _clientService;

        public DistrictController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DistrictPagingRequest request)
        {

            try
            {
                request.PageSize = 1000;
                request.SearchTerm = StringHelper.RefinedSearchTerm(request.SearchTerm);

                var response = await _clientService.Post<DistrictPagingRequest>(ApiPaths.District + "/GetDistricts", request);
                if (response == null)
                {
                    throw new Exception("Không tìm thấy dữ liệu tỉnh");
                }

                return View(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ToastHelper.ShowError(TempData, ex.Message);
                return View(request);
            }
        }


    }
}