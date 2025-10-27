using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        private readonly AppDbContext _context;

        public TitleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Title>> GetAllAsync()
        {
            return await _context.Titles
                                 .FromSqlRaw("SELECT * FROM get_all_movies() LIMIT 10")
                                 .ToListAsync();
        }

        // Example 2: Call string_search(uid, pattern)
        public async Task<IEnumerable<Title>> SearchTitlesAsync(long userId, string pattern)
        {
            var sql = @"
            SELECT tb.*
            FROM string_search({0}, {1}) s
            JOIN title_basics tb ON tb.tconst = s.tconst";
            return await _context.Titles.FromSqlRaw(sql, userId, pattern).ToListAsync();
        }

        public async Task<IEnumerable<TitleDto>> ITitleRepository.SearchTitlesAsyncc(long userId, string query)
        {
            // Provide the wildcard here; function uses ILIKE pattern
            var pattern = $"%{query}%";

            var rows = await _context.TitleSearchRows
                .FromSqlRaw("SELECT * FROM string_search({0}, {1})", userId, pattern)
                .ToListAsync();

            // map to DTOs; if you later need full details, fetch by tconsts
            return rows.Select(r => new TitleDto
            {
                Tconst = r.Tconst,
                PrimaryTitle = r.PrimaryTitle
            });
        }

        Task<IEnumerable<TitleDto>> ITitleRepository.SearchTitlesAsync(long userId, string query)
        {
            throw new NotImplementedException();
        }
    }
}
