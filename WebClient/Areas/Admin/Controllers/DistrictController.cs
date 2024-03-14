using Core.Constants;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.Districts;
using ViewModels.Paging;
using WebClient.Helpers;
using WebClient.Services;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    [Authorize(Roles = "Admin")]
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
                request.SearchTerm = StringHelper.RefinedSearchTerm(request.SearchTerm);

                var response = await _clientService.Post<DistrictPagingRequest>(ApiPaths.Admin + "/District/GetDistricts", request);
                if (response == null)
                {
                    throw new Exception("Server error");
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int districtId)
        {
            try
            {
                if (districtId == 0)
                {
                    ToastHelper.ShowInfo(TempData, "Please choose a district to edit");
                    return RedirectToAction("Index");
                }

                DistrictVM? districtVM = await _clientService.Get<DistrictVM>($"{ApiPaths.Admin}/District/GetDistrictById?districtId={districtId}");
                if (districtVM == null)
                {
                    ToastHelper.ShowWarning(TempData, $"District doesn't exist");
                    return RedirectToAction("Index");
                }

                return View(districtVM);
            }
            catch (Exception)
            {
                ToastHelper.ShowWarning(TempData, $"Error when updating district");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int districtId)
        {
            try
            {
                if (districtId == 0)
                {
                    ToastHelper.ShowInfo(TempData, "Please choose a district to view");
                    return RedirectToAction("Index");
                }

                DistrictVM? districtVM = await _clientService.Get<DistrictVM>($"{ApiPaths.Admin}/District/GetDistrictById?districtId={districtId}");
                if (districtVM == null)
                {
                    ToastHelper.ShowWarning(TempData, $"District doesn't exist");
                    return RedirectToAction("Index");
                }

                return View(districtVM);
            }
            catch (Exception)
            {
                ToastHelper.ShowWarning(TempData, $"Error when updating district");
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(DistrictVM districtVM)
        {
            try
            {
                //PropertyLogger.LogAllProperties(districtVM);
                if (ModelState.IsValid)
                {
                    districtVM = StringHelper.TrimStringProperties<DistrictVM>(districtVM);

                    var response = await _clientService.Post<ResponseVM>($"{ApiPaths.Admin}/District/AddDistrict", districtVM);
                    if (response != null)
                    {

                        if (response.Status)
                        {
                            ToastHelper.ShowSuccess(TempData, response.Message);
                            return RedirectToAction("Index");
                        }

                        ToastHelper.ShowWarning(TempData, response.Message);
                        return View(districtVM);
                    }

                    ToastHelper.ShowError(TempData, "Server error");
                    return RedirectToAction("Error500", "Error", new { area = "" });
                }
            }
            catch (Exception)
            {
            }
            return View(districtVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DistrictVM districtVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    districtVM = StringHelper.TrimStringProperties<DistrictVM>(districtVM);
                    var response = await _clientService.Put<ResponseVM>($"{ApiPaths.Admin}/District/UpdateDistrict", districtVM);
                    if (response == null)
                    {
                        throw new Exception("Update failed");
                    }

                    if (!response.Status)
                    {
                        throw new Exception(response.Message);
                    }
                    ToastHelper.ShowSuccess(TempData, response.Message);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(districtVM);
        }
    }
}
