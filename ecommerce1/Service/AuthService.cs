using ecommerce1.Models;
using ecommerce1.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ecommerce1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<LoginResult> LoginAsync(LoginViewModel model)
        {
            try
            {
                // Validasi credentials (ganti dengan logic database Anda)
                if (!await ValidateUserCredentialsAsync(model.Email, model.Password))
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Email atau password salah"
                    };
                }

                // Buat claims untuk user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim("UserId", "123"), // Ganti dengan ID user dari database
                    new Claim(ClaimTypes.Role, "User") // Sesuaikan dengan role user
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(8)
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    authProperties);

                _logger.LogInformation($"User {model.Email} berhasil login");

                return new LoginResult
                {
                    Success = true,
                    Message = "Login berhasil",
                    ReturnUrl = model.ReturnUrl ?? "/"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saat proses login");
                return new LoginResult
                {
                    Success = false,
                    Message = "Terjadi kesalahan sistem"
                };
            }
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("User berhasil logout");
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            // TODO: Ganti dengan validasi ke database
            // Contoh sederhana (JANGAN gunakan di production):
            await Task.Delay(100); // Simulasi database call

            // Implementasi sebenarnya:
            // var user = await _userRepository.GetByEmailAsync(email);
            // return user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            return email == "admin@example.com" && password == "password123";
        }
    }
}
