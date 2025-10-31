using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Common;
using Application.DTOs;
using Api.Helpers;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class RatingController : ControllerBase
    {
        private readonly IRatingService _service;
        public RatingController(IRatingService service)
        {
            _service = service;
        }
        private long GetUserId()
        {
            return User.GetUserId();
        }

        [HttpPost("{tconst}/{rating}")]
        public async Task<IActionResult> Rate(string tconst, int rating)
        {

            var userId = GetUserId();
            var dto = await _service.RateAsync(userId, tconst, rating);
            return Ok(ApiResponse<RateResponseDto>.Ok(dto, "Rating updated"));
        }
    }
}
