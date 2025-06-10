// Models/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace ecommerce_net.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nama lengkap harus diisi")]
        [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email harus diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password harus diisi")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password minimal 6 karakter")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Konfirmasi password tidak cocok")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Anda harus menyetujui syarat dan ketentuan")]
        public bool AgreeTerms { get; set; }
    }
}