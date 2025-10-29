using System;
namespace Application.Models;

public class NameKnownForTitles
{
    public required string Nconst { get; set; }      
    public required string Tconst { get; set; }     
    public NameBasics? Name { get; set; }
    public TitleBasics? Title { get; set; }
}
