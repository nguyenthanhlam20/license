using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authens
{
    public class SignInVM
    {

        [Required(ErrorMessage = "* Please enter your email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Please enter your password")]
        public string Password { get; set; } = string.Empty;

    }
}
