using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly AppDbContext _context;

        public PeopleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Name>> GetPopularPeopleAsync()
        {
            return await _context.Names
                .FromSqlRaw("SELECT * FROM get_people_full_profile_top20()")
                .ToListAsync();
        }

        public async Task<Name> GetPersonAsync(string nconst)
        {
            return await _context.Names
                .FromSqlRaw("SELECT * FROM get_person_full_profile({0})", nconst)
                .FirstOrDefaultAsync();
        }
    }
}
