using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Constants;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
using ViewModels.Authens;
using Core.Helpers;
using DataAccess.LicensePlateContext;

namespace Repositories.Authen
{
    public class AuthenRepository : IAuthenRepository
    {
        private readonly UserManager<Account> userManager;
        private readonly SignInManager<Account> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IConfiguration configuration;
        private readonly LicensePlateDbContext _context;

        public AuthenRepository(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IConfiguration configuration,
            LicensePlateDbContext context
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<ResponseVM> AccessByEmail(SignInVM request)
        {
            try
            {
                // Find User by Username in database
                var user = await _context.Accounts.SingleOrDefaultAsync(a => a.Email.Equals(request.Email)
                && a.Password.Equals(request.Password)
                && a.IsAccountActive == true);

                if (user != null)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    // create info will stored in JWT, these info will be encode and decode by API application use this JWT
                    List<Claim> claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim(ClaimTypes.GivenName, $"{user.Fullname}"),
                            new Claim("RoleStr", string.Join(";", roles)),

                        };

                    foreach (string role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var accessToken = GenerateJWT(user, claims);

                    return new ResponseVM() { Status = true, Message = accessToken };
                }
                else
                {
                    throw new Exception("Please check your email or password");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ResponseVM() { Status = false, Message = ex.Message };
            }
        }

        private string GenerateJWT(Account user, List<Claim> claims)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(configuration["JWT:ValidIssuer"],
                    configuration["JWT:ValidAudience"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return String.Empty;
            }
        }


        public async Task<bool> IsAccountExisted(string email)
        {
            try
            {
                var account = await _context.Accounts
                    .SingleOrDefaultAsync(a => a.Email == email && a.IsAccountActive == true);
                return account != null;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
