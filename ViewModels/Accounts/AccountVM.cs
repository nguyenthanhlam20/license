using System.ComponentModel.DataAnnotations;

namespace ViewModels.Accounts
{
    public class AccountVM
    {
        [Required(ErrorMessage = "* Nhập tên người dùng")]
        public string? Fullname { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Nhập email")]
        [EmailAddress(ErrorMessage = "* Email sai định dạng")]
        public string? Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? IsAccountActive { get; set; }
        public string? RoleName { get; set; }
        public string? Password { get; set; } 
        public string? ConfirmPassword { get; set; } 

    }
}
