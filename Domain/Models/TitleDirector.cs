using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TitleDirector
    {
        public string Tconst { get; set; } = null!;
        public string Nconst { get; set; } = null!;

        public Title Title { get; set; } = null!;
        public Name Director { get; set; } = null!;
    }
}
