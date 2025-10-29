using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class NameProfession
    {
        public string Nconst { get; set; } = null!;
        public int ProfessionId { get; set; }

        public Name Person { get; set; } = null!;
        public Profession Profession { get; set; } = null!;
    }
}
