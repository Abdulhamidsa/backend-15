using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CelebritySummaryDto
    {
        public string Nconst { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PhotoUrl { get; set; }
        public int? Age { get; set; }
        public decimal? Rating { get; set; }
    }
}
