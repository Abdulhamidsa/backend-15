using Application.DTOs;
using Application.Interfaces;
using Application.RowClasses;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<List<CelebritySummaryRow>> GetPopularAsync()
        {
            return await _context.CelebritySummaryRows
                .FromSqlRaw("SELECT * FROM get_people_full_profile_top20()")
                .ToListAsync();
        }

        public async Task<CelebrityProfileRow?> GetProfileAsync(string nconst)
        {
            return await _context.CelebrityProfileRows
                .FromSqlRaw("SELECT * FROM get_person_full_profile({0})", nconst)
                .FirstOrDefaultAsync();
        }

       
    }
}