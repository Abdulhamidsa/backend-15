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
    }
}
