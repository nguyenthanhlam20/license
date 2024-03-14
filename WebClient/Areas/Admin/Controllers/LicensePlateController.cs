using Core.Constants;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.LicensePlates;
using ViewModels.Paging;
using WebClient.Helpers;
using WebClient.Services;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    [Authorize(Roles = "Admin")]
    public class LicensePlateController : Controller
    {

        private readonly ClientService _clientService;

        public LicensePlateController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(LicensePlatePagingRequest request)
        {

            try
            {
                request.SearchTerm = StringHelper.RefinedSearchTerm(request.SearchTerm);

                var response = await _clientService.Post<LicensePlatePagingRequest>(ApiPaths.Admin + "/LicensePlate/GetLicensePlates", request);
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
        public async Task<IActionResult> Update(int licensePlateId)
        {
            try
            {
                if (licensePlateId == 0)
                {
                    ToastHelper.ShowInfo(TempData, "Please choose a licensePlate to edit");
                    return RedirectToAction("Index");
                }

                LicensePlateVM? licensePlateVM = await _clientService.Get<LicensePlateVM>($"{ApiPaths.Admin}/LicensePlate/GetLicensePlateById?licensePlateId={licensePlateId}");
                if (licensePlateVM == null)
                {
                    ToastHelper.ShowWarning(TempData, $"LicensePlate doesn't exist");
                    return RedirectToAction("Index");
                }

                return View(licensePlateVM);
            }
            catch (Exception)
            {
                ToastHelper.ShowWarning(TempData, $"Error when updating licensePlate");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int licensePlateId)
        {
            try
            {
                if (licensePlateId == 0)
                {
                    ToastHelper.ShowInfo(TempData, "Please choose a licensePlate to view");
                    return RedirectToAction("Index");
                }

                LicensePlateVM? licensePlateVM = await _clientService.Get<LicensePlateVM>($"{ApiPaths.Admin}/LicensePlate/GetLicensePlateById?licensePlateId={licensePlateId}");
                if (licensePlateVM == null)
                {
                    ToastHelper.ShowWarning(TempData, $"LicensePlate doesn't exist");
                    return RedirectToAction("Index");
                }

                return View(licensePlateVM);
            }
            catch (Exception)
            {
                ToastHelper.ShowWarning(TempData, $"Error when updating licensePlate");
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(LicensePlateVM licensePlateVM)
        {
            try
            {
                licensePlateVM.CreatedDate = DateTime.Now;
                //PropertyLogger.LogAllProperties(licensePlateVM);
                if (ModelState.IsValid)
                {
                    licensePlateVM = StringHelper.TrimStringProperties<LicensePlateVM>(licensePlateVM);

                    var response = await _clientService.Post<ResponseVM>($"{ApiPaths.Admin}/LicensePlate/AddLicensePlate", licensePlateVM);
                    if (response != null)
                    {

                        if (response.Status)
                        {
                            ToastHelper.ShowSuccess(TempData, response.Message);
                            return RedirectToAction("Index");
                        }

                        ToastHelper.ShowWarning(TempData, response.Message);
                        return View(licensePlateVM);
                    }

                    ToastHelper.ShowError(TempData, "Server error");
                    return RedirectToAction("Error500", "Error", new { area = "" });
                }
            }
            catch (Exception)
            {
            }
            return View(licensePlateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LicensePlateVM licensePlateVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    licensePlateVM = StringHelper.TrimStringProperties<LicensePlateVM>(licensePlateVM);
                    var response = await _clientService.Put<ResponseVM>($"{ApiPaths.Admin}/LicensePlate/UpdateLicensePlate", licensePlateVM);
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
            return View(licensePlateVM);
        }
    }
}
