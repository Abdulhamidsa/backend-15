using System;
namespace Application.Models;

public class Bookmarks
{
	public required int BookmarkId { get; set; }
	public int UserId { get; set; }
	public string Tconst { get; set; }
    public DateTime Created_at { get; set; }

    public User? User { get; set; }
    public TitleBasics? Title { get; set; }

}
