using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class NameKnownForTitle
    {
        public string Nconst { get; set; } = null!;
        public string Tconst { get; set; } = null!;
        public Name Person { get; set; } = null!;
        public Title Title { get; set; } = null!;
    }
}
