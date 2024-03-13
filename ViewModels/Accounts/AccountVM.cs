using System.ComponentModel.DataAnnotations;

namespace ViewModels.Accounts
{
    public class AccountVM
    {
        [Required(ErrorMessage = "* Please enter fullname")]
        public string? Fullname { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Please enter email")]
        [EmailAddress(ErrorMessage = "* Please enter correct email format 'example@gmail.com'")]
        public string? Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? IsAccountActive { get; set; }
        public string? RoleName { get; set; }
        public string? Password { get; set; } 
        public string? ConfirmPassword { get; set; } 

    }
}
