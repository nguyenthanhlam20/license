using Core.Constants;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.Districts;
using ViewModels.LicensePlates;
using ViewModels.Series;
using WebClient.Services;

namespace WebClient.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[Controller]/[Action]")]
    [Authorize(Roles = "User")]
    public class LicensePlateController : Controller
    {
        private readonly ClientService _clientService; // Service for making API requests


        public LicensePlateController(ClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                List<DistrictVM> districts = await _clientService.Get<List<DistrictVM>>(ApiPaths.District + "/GetAllDistricts");
                List<SeriVM> series = await _clientService.Get<List<SeriVM>>(ApiPaths.Seri + "/GetSeries");

                ViewData["Districts"] = districts;
                ViewData["Series"] = series;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Congrats()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LicensePlateVM licensePlateVM)
        {
            try
            {
                ResponseVM response = await _clientService.Post<ResponseVM>(ApiPaths.LicensePlate + "/AddLicensePlate", licensePlateVM);
             
                
                if(response == null)
                {
                    throw new Exception("Cannot get response value");
                }

                if(response.Status)
                {
                    return RedirectToAction(nameof(Congrats));
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return View();
        }
    }
}
