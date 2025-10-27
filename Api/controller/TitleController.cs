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
        [Authorize]
        
        public async Task<IActionResult> GetAll()
        {
            var titles = await _service.GetAllTitlesAsync();
            return Ok(titles);
        }
    }
}
