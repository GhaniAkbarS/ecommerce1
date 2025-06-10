// Models/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace ecommerce_net.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email harus diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password harus diisi")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}