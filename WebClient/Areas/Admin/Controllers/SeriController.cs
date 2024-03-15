using Core.Constants;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.Series;
using ViewModels.Paging;
using WebClient.Helpers;
using WebClient.Services;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    [Authorize(Roles = "Admin")]
    public class SeriController : Controller
    {

        private readonly ClientService _clientService;

        public SeriController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SeriPagingRequest request)
        {

            try
            {
                request.SearchTerm = StringHelper.RefinedSearchTerm(request.SearchTerm);

                var response = await _clientService.Post<SeriPagingRequest>(ApiPaths.Admin + "/Seri/GetSeries", request);
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
        public async Task<IActionResult> Update(int seriId)
        {
            try
            {
                if (seriId == 0)
                {
                    ToastHelper.ShowInfo(TempData, "Please choose a seri to edit");
                    return RedirectToAction("Index");
                }

                SeriVM? seriVM = await _clientService.Get<SeriVM>($"{ApiPaths.Admin}/Seri/GetSeriById?seriId={seriId}");
                if (seriVM == null)
                {
                    ToastHelper.ShowWarning(TempData, $"Seri doesn't exist");
                    return RedirectToAction("Index");
                }

                return View(seriVM);
            }
            catch (Exception)
            {
                ToastHelper.ShowWarning(TempData, $"Error when updating seri");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int seriId)
        {
            try
            {
                if (seriId == 0)
                {
                    ToastHelper.ShowInfo(TempData, "Please choose a seri to view");
                    return RedirectToAction("Index");
                }

                SeriVM? seriVM = await _clientService.Get<SeriVM>($"{ApiPaths.Admin}/Seri/GetSeriById?seriId={seriId}");
                if (seriVM == null)
                {
                    ToastHelper.ShowWarning(TempData, $"Seri doesn't exist");
                    return RedirectToAction("Index");
                }

                return View(seriVM);
            }
            catch (Exception)
            {
                ToastHelper.ShowWarning(TempData, $"Error when updating seri");
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(SeriVM seriVM)
        {
            try
            {
                //PropertyLogger.LogAllProperties(seriVM);
                if (ModelState.IsValid)
                {
                    seriVM = StringHelper.TrimStringProperties<SeriVM>(seriVM);

                    var response = await _clientService.Post<ResponseVM>($"{ApiPaths.Admin}/Seri/AddSeri", seriVM);
                    if (response != null)
                    {

                        if (response.Status)
                        {
                            ToastHelper.ShowSuccess(TempData, response.Message);
                            return RedirectToAction("Index");
                        }

                        ToastHelper.ShowWarning(TempData, response.Message);
                        return View(seriVM);
                    }

                    ToastHelper.ShowError(TempData, "Server error");
                    return RedirectToAction("Error500", "Error", new { area = "" });
                }
            }
            catch (Exception)
            {
            }
            return View(seriVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SeriVM seriVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    seriVM = StringHelper.TrimStringProperties<SeriVM>(seriVM);
                    var response = await _clientService.Put<ResponseVM>($"{ApiPaths.Admin}/Seri/UpdateSeri", seriVM);
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
            return View(seriVM);
        }
    }
}
