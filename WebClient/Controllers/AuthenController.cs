using Core.Constants;
using WebClient.Helpers;
using WebClient.Models;
using WebClient.Services;
using Core.Constants;
using Core.Enums;
using Core.Helpers;
using ViewModels.Authens;
using ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ViewModels.Accounts;

namespace WebClient.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ClientService _clientService; // Service for making API requests
        private readonly IConfiguration _configuration;

        public AuthenController(
            IConfiguration configuration,
            ClientService clientService
        )
        {
            _configuration = configuration;
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    request.StartDate = DateTime.Now;
                    var apiPath = $"{ApiPaths.Authen}/Register";
                    var response = await _clientService.Post<ResponseVM>(apiPath, request);
                    if (response == null)
                    {
                        throw new Exception("Server error");
                    }
                    if (!response.Status)
                    {
                        throw new Exception(response.Message);
                    }
                    ToastHelper.ShowSuccess(TempData, "Register successful");
                    ToastHelper.ShowSuccess(TempData, "Password has been sent to your email");
                    return RedirectToAction(nameof(SignIn));
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM loginRequest)
        {
            try
            {
                var response = await _clientService.Post<ResponseVM>(ApiPaths.Authen + "/AccessByEmail", loginRequest);
                if (response == null)
                {
                    throw new Exception("Login failed");
                }

                if (response.Status && response.Message != null)
                {
                    // If login is successful, extract and process the access token
                    string accessToken = response.Message; // Extract the access token

                    SessionHelper.SetObject(HttpContext.Session, "AccessToken", accessToken);
                    Console.WriteLine(accessToken);
                    TokenHelper tokenHelper = new TokenHelper();
                    // Get all claims from the access token
                    var allClaims = tokenHelper.GetAllClaims(accessToken);

                    // Extract the "role" claim
                    string roleStr = tokenHelper.FindClaimValueByKey(allClaims, "RoleStr");


                    Role role;
                    if (Enum.TryParse(roleStr, true, out role))
                    {
                        // Process the user's role if needed
                    }

                    // Show a success message
                    ToastHelper.ShowSuccess(TempData, "Login successful!");

                    //Console.WriteLine("role: " + roleStr);

                    UserInfo userInfo = new UserInfo();
                    // Store user information in the UserService
                    userInfo.Role = roleStr; // Store the user's role
                    userInfo.RoleNumber = (int)role; // Store the user's role integer
                    userInfo.Fullname = tokenHelper.FindClaimValueByKey(allClaims, "givenname");
                    userInfo.Email = tokenHelper.FindClaimValueByKey(allClaims, "email");
                    userInfo.UserId = Guid.Parse(tokenHelper.FindClaimValueByKey(allClaims, "nameidentifier"));
                    userInfo.AvatarUrl = tokenHelper.FindClaimValueByKey(allClaims, "AvatarUrl");

                    var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, userInfo.Email),
                                    new Claim(ClaimTypes.Role, roleStr) ,
                                };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    SessionHelper.SetObject<UserInfo>(HttpContext.Session, "UserInfo", userInfo);

                    // Redirect the user based on their role
                    switch (role)
                    {
                        case Role.Admin:
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        case Role.Student:
                            return RedirectToAction("Index", "Home", new { area = "Student" });
                        default:
                            return View("Views/Home/Index.cshtml");
                    }
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(loginRequest);
        }


        [HttpGet]
        [Route("/SignOut")]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                Console.WriteLine("Run here");

                SessionHelper.Remove(HttpContext.Session, "UserInfo");
                SessionHelper.Remove(HttpContext.Session, "AccessToken");

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return RedirectToAction("Index", "Home"); // Redirect to the home page
        }
    }
}
