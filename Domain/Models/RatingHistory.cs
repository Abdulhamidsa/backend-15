using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RatingHistory
    {

        public long UserId { get; set; }
        public string Tconst { get; set; } = null!;
        public short? PreviousRating { get; set; }
        public short NewRating { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Title Title { get; set; } = null!;
    }
}
