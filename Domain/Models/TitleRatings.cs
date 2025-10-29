using System;

namespace Application.Models
{
    public class TitleRating
    {
        public string Tconst { get; set; } = string.Empty;  
        public double AverageRating { get; set; }          
        public int NumVotes { get; set; }                  
    }
}
