using System;
namespace Domain.Models
{

    public class NameKnownForTitles
    {
        public required string Nconst { get; set; }
        public required string Tconst { get; set; }
        public Name Name { get; set; }
        public Title Title { get; set; }
    }
}