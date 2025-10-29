using System;

namespace Application.Models
{
    public class TitleEpisode
    {
        public string Tconst { get; set; } = string.Empty;         
        public string ParentTconst { get; set; } = string.Empty;  
        public int? SeasonNumber { get; set; }                     
        public int? EpisodeNumber { get; set; }                    
    }
}

