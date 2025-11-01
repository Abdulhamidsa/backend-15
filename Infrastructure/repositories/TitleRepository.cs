using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Models;
using Application.Interfaces;
using Application.RowClasses;

namespace Infrastructure.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        private readonly AppDbContext _context;

        public TitleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Title>> GetpopularTitlesAsync()
        {
            return await _context.Titles
                                 .FromSqlRaw("SELECT * FROM get_all_titles()")
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Title>> GetAllSeriesAsync()
        {
            return await _context.Titles
                .FromSqlRaw("SELECT * FROM get_all_series('series')")
                .ToListAsync();
        }

        public async Task<List<TitleRow>> GetTitlesAsync(string? titleType, string? genre)
        {
            var sql = "SELECT * FROM get_all_titles_with_genre({0}, {1}) LIMIT 20";

            return await _context.TitleCatalog
                .FromSqlRaw(sql, titleType, genre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Title>> SearchTitlesAsync(long userId, string pattern)
        {
            var sql = @"
            SELECT tb.*
            FROM string_search({0}, {1}) s
            JOIN title_basics tb ON tb.tconst = s.tconst";
            return await _context.Titles.FromSqlRaw(sql, userId, pattern).ToListAsync();
        }
    }
}
