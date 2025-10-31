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

        [HttpGet("movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _service.GetAllMoviesAsync();
            return Ok(ApiResponse<IEnumerable<TitleDto>>.Ok(movies, "Movies fetched"));
        }

        [HttpGet("series")]
        public async Task<IActionResult> GetAllSeries()
        {
            var series = await _service.GetAllSeriesAsync();
            return Ok(ApiResponse<IEnumerable<TitleDto>>.Ok(series, "Series fetched"));
        }



        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Search query cannot be empty.");

            var userId = User.GetUserId();
            var results = await _service.SearchTitlesAsync(userId, q);

            return Ok(ApiResponse<object>.Ok(results, "Search completed successfully."));
        }
    }
}

