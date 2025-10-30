using System;
namespace Domain.Models
{

    public class TitleAka
    {
        public string TitleId { get; set; } = null!;
        public int Ordering { get; set; }
        public string? Title { get; set; }
        public string? Region { get; set; }
        public string? Language { get; set; }
        public string? Types { get; set; }
        public string? Attributes { get; set; }
        public bool IsOriginalTitle { get; set; }

        public Title TitleRef { get; set; } = null!;
    }
}
