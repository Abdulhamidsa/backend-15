using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TitleCatalogDto
    {
        public string Id { get; set; } = null!;         // maps from Tconst
        public string? Title { get; set; }              // PrimaryTitle
        public int? Year { get; set; }                  // StartYear
        public string? Type { get; set; }               // TitleType (movie, tvSeries,...)
        public string? Poster { get; set; }             // Poster URL
        public string? Genre { get; set; }              // "Action, Sci-Fi"
        
    }
}
