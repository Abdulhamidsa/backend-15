using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TitleDetailsDto
    {
        public string Tconst { get; set; } = null!;
        public string? TitleType { get; set; }
        public string? PrimaryTitle { get; set; }
        public string? OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
        public string? Plot { get; set; }
        public string? Rated { get; set; }
        public DateTime? Released { get; set; }
        public string? Poster { get; set; }
        public int? Metascore { get; set; }
        public int? ImdbVotes { get; set; }
        public decimal? BoxOffice { get; set; }
        public string? Awards { get; set; }

        public RatingDto? Rating { get; set; }

        public List<string> Genres { get; set; } = new();
        public List<string> People { get; set; } = new();
    }
}

