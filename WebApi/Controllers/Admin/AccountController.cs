using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Repositories.Accounts;
using Core.Helpers;
using ViewModels.Paging;
using ViewModels.Accounts;
using ViewModels;
using DataAccess.Models;
using Core.Constants;
using WebApi.Services;
using Humanizer;

namespace WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/Admin/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountController(IAccountRepository accountRepository, IMapper mapper, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _emailService = emailService;
        }


        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] AccountPagingRequest request)
        {
            AccountPagingRequest response = await _accountRepository.GetAll(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountByEmail([FromQuery] string email)
        {
            AccountVM account = await _accountRepository.GetAccountByEmail(email);
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountManual([FromBody] AccountVM request)
        {
            try
            {
                var account = _mapper.Map<Account>(request);
                var roles = new List<string>() { RoleConstants.User };
                var response = await _accountRepository.CreateAccountManualAsync(account, roles);

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
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseVM { Status = false, Message = ex.Message });
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccountManual([FromBody] AccountVM request)
        {
            try
            {
                var account = _mapper.Map<Account>(request);
                var roles = new List<string>() { RoleConstants.User };
                var response = await _accountRepository.UpdateAccountManualAsync(account);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseVM { Status = false, Message = ex.Message });
            }

        }

    }
}
