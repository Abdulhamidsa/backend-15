using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TitlesController : ControllerBase
    {
        private readonly TitleService _service;

        public TitlesController(TitleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var titles = await _service.GetAllTitlesAsync();
            return Ok(titles);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] long userId, [FromQuery] string q)
        {
            var results = await _service.SearchAsync(userId, q);
            return Ok(results);
        }
    }
}
