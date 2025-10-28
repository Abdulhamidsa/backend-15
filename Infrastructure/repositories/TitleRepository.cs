using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Models;
using Application.Interfaces;

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
