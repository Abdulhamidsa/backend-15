
using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITitleService
    {
        Task<IEnumerable<TitleDto>> GetAllTitlesAsync();
        Task<IEnumerable<TitleDto>> SearchTitlesAsync(long userId, string query);
    }
}
