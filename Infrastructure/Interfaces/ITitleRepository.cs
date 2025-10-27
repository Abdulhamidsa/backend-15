using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface ITitleRepository
    {
        Task<IEnumerable<Title>> GetAllAsync();
    }
}
