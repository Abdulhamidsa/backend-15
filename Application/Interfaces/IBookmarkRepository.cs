using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBookmarkRepository
    {
        Task AddAsync(long userId, string tconst);
        Task RemoveAsync(long userId, string tconst);
        Task<bool> ExistsAsync(long userId, string tconst);
        Task<List<BookmarkDto>> GetUserBookmarksAsync(long userId);
    }
}
