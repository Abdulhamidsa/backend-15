using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly AppDbContext _context;
        public BookmarkRepository(AppDbContext context) { _context = context; }

        public async Task AddAsync(long userId, string tconst)
        {
            if (string.IsNullOrWhiteSpace(tconst))
                throw new ArgumentException("tconst cannot be null or empty");

            await _context.Database.ExecuteSqlRawAsync(
                "SELECT add_bookmark({0}, {1})", userId, tconst);
        }

        public async Task RemoveAsync(long userId, string tconst)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "SELECT remove_bookmark({0}, {1})", userId, tconst);
        }

        public async Task<bool> ExistsAsync(long userId, string tconst)
        {
            return await _context.Bookmarks
                .AsNoTracking()
                .AnyAsync(b => b.UserId == userId && b.Tconst == tconst);
        }

        public async Task<List<BookmarkDto>> GetUserBookmarksAsync(long userId)
        {
            return await _context.Bookmarks
                .AsNoTracking()
                .Where(b => b.UserId == userId)
                .Join(
                    _context.Titles.AsNoTracking(),
                    b => b.Tconst,
                    t => t.Tconst,
                    (b, t) => new BookmarkDto
                    {
                        Tconst = t.Tconst,
                        PrimaryTitle = t.PrimaryTitle ?? string.Empty,
                        StartYear = t.StartYear,
                        CreatedAt = b.CreatedAt
                    }
                )
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}