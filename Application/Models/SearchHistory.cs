using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SearchHistory
    {
        public long SearchId { get; set; }
        public long UserId { get; set; }
        public string SearchQuery { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
    }
}
