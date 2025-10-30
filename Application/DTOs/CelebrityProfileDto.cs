using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CelebrityProfileDto
    {
        public string Nconst { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public string? Professions { get; set; }
        public decimal? Rating { get; set; }
        public string? KnownFor { get; set; }
        public long? CreditsCount { get; set; }
        public string? Awards { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Bio { get; set; }
        public long? TotalVotes { get; set; }
    }
}
