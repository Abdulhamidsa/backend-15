namespace Domain.Models;

public class User
{
    public long Id { get; set; }

    public required string Email { get; set; }
    public required string Username { get; set; }

    public required string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relationships
    public ICollection<SearchHistory> SearchHistories { get; set; } = new List<SearchHistory>();
    public ICollection<UserRating> Ratings { get; set; } = new List<UserRating>();
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
    public ICollection<RatingHistory> RatingHistories { get; set; } = new List<RatingHistory>();
}
