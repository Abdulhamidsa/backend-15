using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserRating
    {
        public long UserId { get; set; }
        public string Tconst { get; set; } = null!;
        public int RatingValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Title Title { get; set; } = null!;
    }
}
