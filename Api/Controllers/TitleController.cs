using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs;
using Application.Interfaces;
using Application.Common;

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

        [HttpGet ("movies")]
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
    

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] long userId, [FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest(ApiResponse<IEnumerable<TitleDto>>.Fail("Search query cannot be empty"));
            }

            var results = await _service.SearchTitlesAsync(userId, q);
            return Ok(ApiResponse<IEnumerable<TitleDto>>.Ok(results, "Search completed successfully"));
        }
    }
}
