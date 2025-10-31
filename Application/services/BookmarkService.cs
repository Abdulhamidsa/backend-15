using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _repository;

        public BookmarkService(IBookmarkRepository repository)
        {
            _repository = repository;
        }

// toggle bookmark
        public async Task<bool> ToggleBookmarkAsync(long userId, string tconst)
        {
            if (await _repository.ExistsAsync(userId, tconst))
            {
                await _repository.RemoveAsync(userId, tconst);
                return false;
            }
            else
            {
                await _repository.AddAsync(userId, tconst);
                return true;
            }
        }
// get user bookmarks
        public async Task<List<BookmarkDto>> GetUserBookmarksAsync(long userId)
        {
            return await _repository.GetUserBookmarksAsync(userId);
        }
    }
}
