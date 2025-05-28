using ecommerce1.Models;

namespace ecommerce1.Services
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
    }
}