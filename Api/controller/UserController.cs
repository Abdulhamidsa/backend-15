using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _service;
    public AuthController(IUserService service) => _service = service;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var resp = await _service.RegisterAsync(request);

        // TODO JWT: if you switch to cookie-based auth:
        // Response.Cookies.Append("access_token", resp.AccessToken!, new CookieOptions {
        //     HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict
        // });

        return Ok(resp);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var resp = await _service.LoginAsync(request);

        // TODO JWT: same cookie note as in register

        return Ok(resp);
    }
}
