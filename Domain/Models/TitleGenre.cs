using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TitleGenre
    {
        public string Tconst { get; set; } = null!;
        public int GenreId { get; set; }

        public Title Title { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
    }
}
