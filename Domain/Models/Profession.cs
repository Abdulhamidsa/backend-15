using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Profession
    {
        public int ProfessionId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<NameProfession> NameProfessions { get; set; } = new List<NameProfession>();
    }
}
