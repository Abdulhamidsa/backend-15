using System;
namespace Domain.Models
{
    public class Genre
    {
        public required int GenreId { get; set; }
        public string? Name { get; set; }
        public ICollection<TitleGenre>? TitleGenres { get; set; }
    }
}

