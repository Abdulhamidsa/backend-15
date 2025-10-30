using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TitlePrincipal
    {
        public string Tconst { get; set; } = null!;
        public string Nconst { get; set; } = null!;
        public int Ordering { get; set; }
        public string? Category { get; set; }
        public string? Job { get; set; }

        public Title Title { get; set; } = null!;
        public Name Person { get; set; } = null!;
    }
}
