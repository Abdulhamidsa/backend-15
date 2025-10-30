using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Common; 
using Microsoft.AspNetCore.Http; 

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _service.RegisterAsync(request);

            // Set access token in secure HTTP-only cookie
            if (result.AccessToken != null)
            {
                Response.Cookies.Append("access_token", result.AccessToken, new CookieOptions
                {
                    HttpOnly = true,     // Prevents JavaScript access
                    Secure = true,       // Set to true in production (HTTPS only)
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60) // Match token expiry
                });
            }

            return Ok(ApiResponse<AuthResponse>.Ok(result, "User registered successfully"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _service.LoginAsync(request);

            if (result.AccessToken != null)
            {
                Response.Cookies.Append("access_token", result.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });
            }

            return Ok(ApiResponse<AuthResponse>.Ok(result, "Login successful"));
        }
    }
}
