using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PersonMetadata
    {
        // PK and FK to Name (name_basics)
        public string Nconst { get; set; } = null!;

        public string? PhotoUrl { get; set; }
        public string? Bio { get; set; }

        // Navigation to Name
        public Name Person { get; set; } = null!;
    }
}
