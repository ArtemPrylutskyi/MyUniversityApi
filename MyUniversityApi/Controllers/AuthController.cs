using Microsoft.AspNetCore.Mvc;
using MyUniversityApi.Models;
using MyUniversityApi.Services;

namespace MyUniversityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var (success, message) = await _authService.RegisterAsync(request);

            if (!success)
            {
                return BadRequest(new { message = message });
            }

            return Ok(new { message = message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var (success, token) = await _authService.LoginAsync(request);

            if (!success)
            {
                return Unauthorized(new { message = token });
            }

            return Ok(new { token = token });
        }
    }
}