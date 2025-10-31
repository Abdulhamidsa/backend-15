namespace Application.DTOs
{
    public class BookmarkDto
    {
        public string Tconst { get; set; } = null!;
        public string? PrimaryTitle { get; set; } 
        public int? StartYear { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
