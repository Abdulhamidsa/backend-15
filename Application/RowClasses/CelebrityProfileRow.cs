using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RowClasses
{
    public class CelebrityProfileRow
    {
        public string Nconst { get; set; } = null!;
        public string PrimaryName { get; set; } = null!;
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public string? Professions { get; set; }
        public decimal? Weighted_Avg { get; set; }
        public string? KnownFor { get; set; }
        public long? CreditsCount { get; set; }
        public string? AggregatedAwards { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Bio { get; set; }
        public long? Total_Votes { get; set; }
    }
}
