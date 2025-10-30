using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _repo;

        public PeopleService(IPeopleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CelebritySummaryDto>> GetPopularAsync()
        {
            var rows = await _repo.GetPopularAsync();

            return rows.Select(r => new CelebritySummaryDto
            {
                Nconst = r.Nconst,
                Name = r.PrimaryName,
                PhotoUrl = r.Photo_Url,
                Age = r.Age,
                Rating = r.Weighted_Avg
            });
        }

        public async Task<CelebrityProfileDto?> GetProfileAsync(string nconst)
        {
            var row = await _repo.GetProfileAsync(nconst);
            if (row == null)
                return null;

            return new CelebrityProfileDto
            {
                Nconst = row.Nconst,
                Name = row.PrimaryName,
                BirthYear = row.BirthYear,
                DeathYear = row.DeathYear,
                Professions = row.Professions,
                Rating = row.Weighted_Avg,
                KnownFor = row.KnownFor,
                CreditsCount = row.CreditsCount,
                Awards = row.AggregatedAwards,
                PhotoUrl = row.PhotoUrl,
                Bio = row.Bio,
                TotalVotes = row.Total_Votes
            };
        }
    }
}