using FCMS.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Authens
{
    public class RegisterVM
    {

        [Required(ErrorMessage = "* Please enter your fullname")]
        public string Fullname { get; set; } = string.Empty;
        [Required(ErrorMessage = "* Please enter your email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "* Please enter your password")]
        public string Password { get; set; } = string.Empty;
    }
}
