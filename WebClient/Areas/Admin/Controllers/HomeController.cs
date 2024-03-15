using WebClient.Helpers;
using WebClient.Models;
using WebClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using WebClient;
using ViewModels;
using Core.Constants;


namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        private readonly ClientService _clientService;

        public HomeController(ClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            List<RoleVM> roles = SessionHelper.GetObject<List<RoleVM>>(HttpContext.Session, "Roles");

            if (roles == null)
            {
                roles = await _clientService.Get<List<RoleVM>>(ApiPaths.Admin + "/Role/GetRoles");
            }
            SessionHelper.SetObject<List<RoleVM>>(HttpContext.Session, "Roles", roles);
            return RedirectToAction("Index", "User");
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
