using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBookmarkService
    {
        Task<bool> ToggleBookmarkAsync(long userId, string tconst);
        Task<List<BookmarkDto>> GetUserBookmarksAsync(long userId);
    }
}
