using Application.Interfaces;
using Application.RowClasses;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        // NEW: get basic details JSON for a single title
        public async Task<string?> GetTitleInfoById(string tconst)
        {
            // We'll execute: SELECT get_title_basic_details(@p_tconst);

            await using var conn = _context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT get_title_basic_details(@p_tconst)";
            var param = cmd.CreateParameter();
            param.ParameterName = "p_tconst";
            param.Value = tconst;
            cmd.Parameters.Add(param);

            var result = await cmd.ExecuteScalarAsync();
            if (result == null || result == DBNull.Value)
                return null;

            return result.ToString();
        }
    }
}
 
