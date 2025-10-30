using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class NameRating
    {
        public string Nconst { get; set; } = null!;
        public decimal? AvgRating { get; set; }
        public long? TotalVotes { get; set; }

        public Name Person { get; set; } = null!;
    }
}
