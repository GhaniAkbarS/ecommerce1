using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ecommerce1.Models;
using ecommerce1.Services;

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

                // Ambil data user (simulasi - ganti dengan database query)
                var userData = await GetUserDataAsync(model.Email);

                // Buat claims untuk user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userData.Name),
                    new Claim(ClaimTypes.Email, userData.Email),
                    new Claim("UserId", userData.Id.ToString()),
                    new Claim(ClaimTypes.Role, userData.Role),
                    new Claim("LoginTime", DateTime.UtcNow.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(8),
                    AllowRefresh = true
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    authProperties);

                _logger.LogInformation($"User {model.Email} berhasil login pada {DateTime.Now}");

                return new LoginResult
                {
                    Success = true,
                    Message = "Login berhasil",
                    ReturnUrl = string.IsNullOrEmpty(model.ReturnUrl) ? "/" : model.ReturnUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saat proses login untuk user: {Email}", model.Email);
                return new LoginResult
                {
                    Success = false,
                    Message = "Terjadi kesalahan sistem"
                };
            }
        }

        private async Task<UserData> GetUserDataAsync(string email)
        {
            // TODO: Ganti dengan query database sebenarnya
            await Task.Delay(50); // Simulasi database call

            // Simulasi data user
            return new UserData
            {
                Id = 1,
                Name = "Admin User",
                Email = email,
                Role = "Admin"
            };
        }

        private class UserData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
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