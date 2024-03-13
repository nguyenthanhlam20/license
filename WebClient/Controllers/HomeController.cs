
using WebClient.Models;
using WebClient.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Core.Helpers;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        // Dependency injection of services
        private readonly ILogger<HomeController> _logger;
        private readonly ClientService _clientService; // Service for making API requests

        public HomeController(ClientService clientService, ILogger<HomeController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
               
                return View();
            }
            catch (Exception ex)
            {

                return View();
               
            }
        }
    }
}