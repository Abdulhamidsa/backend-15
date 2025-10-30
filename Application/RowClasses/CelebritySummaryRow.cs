using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RowClasses
{
    public class CelebritySummaryRow
    {
        public string Nconst { get; set; } = null!;
        public string PrimaryName { get; set; } = null!;
        public string? Photo_Url { get; set; }
        public int? Age { get; set; }
        public decimal? Weighted_Avg { get; set; }
    }
}
