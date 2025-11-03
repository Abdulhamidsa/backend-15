namespace Application.DTOs
{
    public class TitleDto
    {
        public required string Tconst { get; set; }
        public string? PrimaryTitle { get; set; }
        public int? StartYear { get; set; }
        public string? TitleType { get; set; }
        public string? Poster { get; set; }

    }
}
