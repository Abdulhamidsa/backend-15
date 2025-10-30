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
        public Task<IEnumerable<CelebritySummaryDto>> GetPopularAsync()
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

        public Task<CelebrityProfileDto?> GetProfileAsync(string nconst)
        {
            throw new NotImplementedException();
        }
    }
}
