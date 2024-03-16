using Microsoft.AspNetCore.Mvc;
using WebClient.Services;
using Core.Helpers;
using ViewModels.Paging;
using ViewModels.Accounts;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Core.Constants;
using DataAccess.Models;
using Core.Helpers;
using WebClient.Helpers;
using ViewModels.Accounts;
using WebClient;
using ViewModels.LicensePlates;
using WebClient.Models;
using ViewModels;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        private readonly ClientService _clientService;

        public UserController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(AccountPagingRequest request)
        {
            try
            {

                request.SearchTerm = StringHelper.RefinedSearchTerm(request.SearchTerm);
                request.RoleName = RoleConstants.User;

                AccountPagingRequest response = await _clientService.Post<AccountPagingRequest>(ApiPaths.Admin + "/Account/GetAll", request);
                if (response == null)
                {
                    ToastHelper.ShowError(TempData, "Không nhận được dữ liệu trả về");
                    throw new Exception();
                }

                return View(response);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return View(request);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    request.StartDate = DateTime.Now;
                    var apiPath = $"{ApiPaths.Admin}/Account/CreateAccountManual";
                    var response = await _clientService.Post<ResponseVM>(apiPath, request);
                    if (response == null)
                    {
                        throw new Exception("Server error");
                    }
                    if (!response.Status)
                    {
                        throw new Exception(response.Message);
                    }
                    ToastHelper.ShowSuccess(TempData, response.Message);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AccountVM request)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var apiPath = $"{ApiPaths.Admin}/Account/UpdateAccountManual";
                    ResponseVM response = await _clientService.Put<ResponseVM>(apiPath, request);
                    if (response == null)
                    {
                        throw new Exception("Server error");
                    }

                    if (!response.Status)
                    {

                        throw new Exception(response.Message);
                    }
                    ToastHelper.ShowSuccess(TempData, response.Message);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
                return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Update([FromQuery] string email)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    throw new Exception("Choose a User to view their information");
                }


                AccountVM? account = await _clientService.Get<AccountVM>($"{ApiPaths.Admin}/Account/GetAccountByEmail?email={email}");

                if (account == null)
                {
                    throw new Exception($"User with '{email}' doesn't exist.");
                }

                AccountVM updateAccountVM = new AccountVM()
                {
                    Email = email,
                    Fullname = account.Fullname,
                    IsAccountActive = account.IsAccountActive,
                    RoleName = account.RoleName,
                };
                return View(updateAccountVM);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowInfo(TempData, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromQuery] string email)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    throw new Exception("Choose a user to view their information");
                }
             

                AccountVM? account = await _clientService.Get<AccountVM>($"{ApiPaths.Admin}/Account/GetAccountByEmail?email={email}");
                List<LicensePlateVM> licensePlates = await _clientService.Get<List<LicensePlateVM>>($"{ApiPaths.Admin}/LicensePlate/GetLicensePlates?email={email}");

                ViewData["LicensePlates"] = licensePlates;
                if (account == null)
                {
                    throw new Exception($"User with '{email}' doesn't exist.");
                }

                AccountVM updateAccountVM = new AccountVM()
                {
                    Email = email,
                    Fullname = account.Fullname,
                    IsAccountActive = account.IsAccountActive,
                    RoleName = account.RoleName,
                };

                return View(updateAccountVM);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowInfo(TempData, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
