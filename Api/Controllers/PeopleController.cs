using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _service;

        public PeopleController(IPeopleService service)
        {
            _service = service;
        }

        // GET /api/people/popular
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopular()
        {
            var people = await _service.GetPopularAsync();
            return Ok(people);
        }

        // GET /api/people/{nconst}
        [HttpGet("{nconst}")]
        public async Task<IActionResult> GetById(string nconst)
        {
            if (string.IsNullOrWhiteSpace(nconst))
                return BadRequest("nconst is required.");

            var person = await _service.GetProfileAsync(nconst);

            if (person == null)
                return NotFound();

            return Ok(person);
        }
    }
}
