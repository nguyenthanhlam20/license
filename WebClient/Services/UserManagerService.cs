using Microsoft.AspNetCore.Identity;

namespace FCMS.Client.Services
{
    public class UserManagerService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManagerService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SignInResult> SignInUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Sign-in successful
                    return result;
                }
            }

            // Sign-in failed
            return SignInResult.Failed;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
