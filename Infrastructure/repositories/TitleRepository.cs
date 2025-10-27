using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Models;
using Infrastructure.Interfaces;
{
    
}
namespace Infrastructure.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        private readonly AppDbContext _context;

        public TitleRepository(AppDbContext context)
        {
            _context = context;
        }


        async Task<IEnumerable<Title>> GetAllAsync()
        {
            return await _context.Titles.FromSqlRaw("SELECT * FROM get_all_movies()").ToListAsync();
        }


    }
}
