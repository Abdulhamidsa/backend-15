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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var titles = await _service.GetAllTitlesAsync();
            return Ok(titles);
        }
    }
}
