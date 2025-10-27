using Application.DTOs;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ITitleRepository
    {
        Task<IEnumerable<Title>> GetAllAsync();
         Task<IEnumerable<TitleDto>> SearchTitlesAsync(long userId, string query);
    }
}
