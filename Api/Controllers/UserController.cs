using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

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

            // Set access token in httpOnly cookie (exact name: accesstoken)
            if (result.AccessToken != null)
            {
                Response.Cookies.Append("accesstoken", result.AccessToken, new CookieOptions
                {
                    HttpOnly = true,     // JavaScript cannot read it - security
                    Secure = true,       // Required when SameSite=None
                    SameSite = SameSiteMode.None, // Required for cross-origin
                    Expires = DateTimeOffset.UtcNow.AddDays(7) // 7 days session
                });
            }

            var userData = new
            {
                userId = result.UserId.ToString(),
                email = result.Email,
                username = result.Username
            };

            return Ok(ApiResponse<object>.Ok(userData, "User registered successfully"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _service.LoginAsync(request);

            // Set access token in httpOnly cookie (exact name: accesstoken)
            if (result.AccessToken != null)
            {
                Response.Cookies.Append("accesstoken", result.AccessToken, new CookieOptions
                {
                    HttpOnly = true,     // JavaScript cannot read it - security
                    Secure = true,       // Required when SameSite=None
                    SameSite = SameSiteMode.None, // Required for cross-origin
                    Expires = DateTimeOffset.UtcNow.AddDays(7) // 7 days session
                });
            }

            var userData = new
            {
                userId = result.UserId.ToString(),
                email = result.Email,
                username = result.Username
            };

            return Ok(ApiResponse<object>.Ok(userData, "Login successful"));
        }





        // GET /api/Auth/me - Check if user has valid session from cookie
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            // Extract user claims from validated JWT token
            var userId = User.FindFirst("id")?.Value ?? User.FindFirst("sub")?.Value;
            var email = User.FindFirst("email")?.Value;
            var username = User.FindFirst("username")?.Value;

            if (userId == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Not authenticated"
                });
            }

            return Ok(new
            {
                success = true,
                data = new
                {
                    userId,
                    email,
                    username
                }
            });
        }

        // POST /api/Auth/logout - Clear the session cookie
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accesstoken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });

            return Ok(new
            {
                success = true,
                message = "Logged out successfully"
            });
        }


    }
}
