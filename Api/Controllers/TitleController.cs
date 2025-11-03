using Api.Helpers;
using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TitlesController : ControllerBase
    {
        private readonly ITitleService _service;

        public TitlesController(ITitleService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTitles()
        {
            var titles = await _service.GetAllTitlesAsync();
            return Ok(ApiResponse<IEnumerable<TitleDto>>.Ok(titles, "Titles fetched"));
        }


        [HttpGet("catalog")]
        public async Task<IActionResult> GetTitles([FromQuery] string? type, [FromQuery] string? genre)
        {
            var data = await _service.GetTitlesAsync(type, genre);

            return Ok(ApiResponse<object>.Ok(data, "OK"));
        }

        [HttpGet("{id}/basic")]
        public async Task<IActionResult> GetTitleById([FromRoute] string id)
        {
            var json = await _service.GetTitleInfoById(id);

            if (json == null)
                return NotFound(ApiResponse<string>.Fail("Title not found or unavailable"));

            return Ok(ApiResponse<object>.Ok(
                System.Text.Json.JsonSerializer.Deserialize<object>(json)!,
                "OK"
            ));
        }


        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Search query cannot be empty.");

            try
            {
                //  Use our helper to extract user ID
                var userId = User.GetUserId();

                //  Call our service with authenticated user's ID
                var results = await _service.SearchTitlesAsync(userId, q);

                //  Return a consistent success response
                return Ok(ApiResponse<object>.Ok(results, "Search completed successfully."));
            }
            catch (UnauthorizedAccessException ex)
            {
                // Missing or invalid claim
                return Unauthorized(ApiResponse<string>.Fail(ex.Message));
            }
            catch (FormatException)
            {
                // Claim not convertible to long
                return Unauthorized(ApiResponse<string>.Fail("Invalid user ID format in token."));
            }
            catch (Exception ex)
            {
                // Any unexpected errors
                return StatusCode(500, ApiResponse<string>.Fail($"An unexpected error occurred: {ex.Message}"));
            }
        }
    }
}
