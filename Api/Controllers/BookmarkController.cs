using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Common;
using Api.Helpers;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkService _service;

        public BookmarksController(IBookmarkService service)
        {
            _service = service;
        }

        private long GetUserId() => User.GetUserId();

        // POST /api/bookmarks/toggle/{tconst}
        [HttpPost("toggle/{tconst}")]
        public async Task<IActionResult> ToggleBookmark(string tconst)
        {
            if (string.IsNullOrEmpty(tconst))
                return BadRequest(ApiResponse<string>.Fail("Missing tconst parameter."));

            var userId = GetUserId();
            bool isNowBookmarked = await _service.ToggleBookmarkAsync(userId, tconst);
            var message = isNowBookmarked ? "Bookmark added" : "Bookmark removed";

            var response = ApiResponse<object>.Ok(
                new { Bookmarked = isNowBookmarked },
                message
            );

            return Ok(response);
        }

        // GET /api/bookmarks
        [HttpGet]
        public async Task<IActionResult> GetBookmarks()
        {
            var userId = GetUserId();
            var bookmarks = await _service.GetUserBookmarksAsync(userId);

            var response = ApiResponse<object>.Ok(bookmarks, "Bookmarks retrieved successfully.");
            return Ok(response);
        }
    }
}
