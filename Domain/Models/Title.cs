namespace Domain.Models;

public class Title
{
    public required string Tconst { get; set; }
    public string? TitleType { get; set; }
    public string? PrimaryTitle { get; set; }
    public string? OriginalTitle { get; set; }
    public bool IsAdult { get; set; }
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public int? RuntimeMinutes { get; set; }
    public string? Awards { get; set; }
    public string? Plot { get; set; }
    public string? Rated { get; set; }
    public DateTime? Released { get; set; } 
    public string? Poster { get; set; }
    public int? ImdbVotes { get; set; }
    public decimal? BoxOffice { get; set; }
    public string? Website { get; set; }
    public int? Metascore { get; set; }

    // Relationships
    public ICollection<TitleAka> AlternateTitles { get; set; } = new List<TitleAka>();
    public ICollection<TitleGenre> TitleGenres { get; set; } = new List<TitleGenre>();
    public ICollection<TitleDirector> Directors { get; set; } = new List<TitleDirector>();
    public ICollection<TitleWriter> Writers { get; set; } = new List<TitleWriter>();
    public ICollection<TitlePrincipal> Principals { get; set; } = new List<TitlePrincipal>();
    public ICollection<TitleCharacter> Characters { get; set; } = new List<TitleCharacter>();
    public ICollection<TitleEpisode> Episodes { get; set; } = new List<TitleEpisode>();
    public ICollection<NameKnownForTitle> KnownBy { get; set; } = new List<NameKnownForTitle>();
}