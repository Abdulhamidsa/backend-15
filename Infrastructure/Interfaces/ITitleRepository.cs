using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Application.Interfaces
{
    public interface ITitleRepository
    {
        Task<IEnumerable<Title>> GetAllAsync();
    }
}
