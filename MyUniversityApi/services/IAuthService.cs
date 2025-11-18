using MyUniversityApi.Models;

namespace MyUniversityApi.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(UserRegisterRequest request);
        Task<(bool Success, string Token)> LoginAsync(UserLoginRequest request);
    }
}