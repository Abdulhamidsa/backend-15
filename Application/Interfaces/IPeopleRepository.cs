using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<Name>> GetPopularAsync();
        Task<Name> GetProfileAsync(string nconst);
    }
}
