using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Name
    {
        public string Nconst { get; set; } = null!;
        public string PrimaryName { get; set; } = null!;
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }

        // Relationships
        public ICollection<NameProfession> Professions { get; set; } = new List<NameProfession>();
        public ICollection<TitlePrincipal> Principals { get; set; } = new List<TitlePrincipal>();
        public ICollection<TitleCharacter> Characters { get; set; } = new List<TitleCharacter>();
        public ICollection<TitleDirector> DirectedTitles { get; set; } = new List<TitleDirector>();
        public ICollection<TitleWriter> WrittenTitles { get; set; } = new List<TitleWriter>();
        public ICollection<NameKnownForTitle> KnownForTitles { get; set; } = new List<NameKnownForTitle>();
        public NameRating? NameRating { get; set; }
    }
}
