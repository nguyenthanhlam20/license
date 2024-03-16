
using WebClient.Models;
using WebClient.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebClient.Helpers;
using DataAccess.Models;
using ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Core.Constants;
using ViewModels.LicensePlates;

namespace WebClient.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[Controller]/[Action]")]
    [Authorize(Roles = "User")]
    public class MyLicensePlateController : Controller
    {
        // Dependency injection of services
        private readonly ILogger<MyLicensePlateController> _logger;
        private readonly ClientService _clientService; // Service for making API requests


        public MyLicensePlateController(ClientService clientService, ILogger<MyLicensePlateController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            UserInfo userInfo = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "UserInfo");

            List<LicensePlateVM> response = await _clientService.Get<List<LicensePlateVM>>($"{ApiPaths.User}/LicensePlate/GetLicensePlates?email={userInfo.Email}");

            return View(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("Errors/Error404");
            }
            return View();
        }
    }
}