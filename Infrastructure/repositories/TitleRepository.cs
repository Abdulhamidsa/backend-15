using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Models;
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
    }
}
