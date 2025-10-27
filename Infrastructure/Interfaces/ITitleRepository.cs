using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface ITitleRepository
    {
        Task<IEnumerable<Title>> GetAllAsync();
    }
}
