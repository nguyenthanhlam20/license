using Core.Helpers;
using DataAccess.Models;
using Repositories.Authen;
using ViewModels.Authens;
using ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Accounts;
using Repositories.Accounts;
using Core.Constants;
using AutoMapper;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/Authen/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenRepository _authenRepository;
        private readonly IAccountRepository _acccountRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;


        public AuthenController(IAuthenRepository authenRepository, IAccountRepository accountRepository, IEmailService emailService,
            IMapper mapper)
        {
            _authenRepository = authenRepository;
            _acccountRepository = accountRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AccessByEmail([FromBody] SignInVM request)
        {
            try
            {
                return Ok(await _authenRepository.AccessByEmail(request));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AccountVM request)
        {
            try
            {
                var roles = new List<string>() { RoleConstants.User };
                var account = _mapper.Map<Account>(request);
                var response = await _acccountRepository.CreateAccountManualAsync(account, roles);


                if (response != null && response.Status)
                {
                    string link = "https://localhost:7173/Authen/SignIn";
                    string Subject = "[LOGIN INVITATION] - Welcome To Online Exam Website";
                    string Body = @"
                            <!DOCTYPE html>
                            <html>
                            <head>
                                <title>Invitation Letter</title>
                                <style>
                          

                                </style>
                            </head>
                            <body>
                                <div class=""container"">
                                    <h3>Hi " + request.Fullname + @", </h3>
                                    <p>You have an invitation to use online exam website.</p>
                                    <p>Please use your email: <strong>" + request.Email + @"</strong> and password: <strong>" + response.Message + @"</strong> to login!</p>
                                    <div>
                                        <span>Our website: </span>
                                        <a href='" + link + @"' ><span>Click here</span></a>
                                    </div>
                                </div>
                            </body>
                            </html>
                            ";
                    // Send Email to user
                    await _emailService.SendHtmlEmailAsync(request.Email, Subject, Body);


                    response.Message = "Create student successful";

                    return Ok(response);
                }
                else
                {
                    throw new Exception("Register failed");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Ok(new ResponseVM() { Status = false, Message = ex.Message });
            }
        }
    }
}
