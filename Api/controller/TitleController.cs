using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;








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
            var titles = await _service.GetAllMoviesAsync();
            return Ok(titles);
        }

        [HttpGet("series")]
        public async Task<IActionResult> GetAllSeries()
        {
            var series = await _service.GetAllSeriesAsync();
            return Ok(series);
        }
    

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] long userId, [FromQuery] string q)
        {
            var results = await _service.SearchTitlesAsync(userId, q);
            return Ok(results);
        }
    }
}
