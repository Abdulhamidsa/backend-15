using System;
namespace Domain.Models
{

    public class TitleAkas
    {
        public string TitleId { get; set; } = null!;       
        public int Ordering { get; set; }             // Display order
        public string Title { get; set; }             // Alternate title
        public string? Region { get; set; }                    // Country/region code
        public string? Language { get; set; }                  // Language code
        public string? Types { get; set; }                     // e.g., "alternative", "working"
        public string? Attributes { get; set; }                // e.g., "IMAX", "DVD title"
        public bool IsOriginalTitle { get; set; }              // True if it's the original title

        public Title TitleRef { get; set; } = null!;
    }
}
