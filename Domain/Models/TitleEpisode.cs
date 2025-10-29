using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TitleEpisode
    {
        public string Tconst { get; set; } = null!;
        public string? ParentTconst { get; set; }
        public int? SeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }

        public Title Title { get; set; } = null!;
        public Title? ParentTitle { get; set; }
    }
}
