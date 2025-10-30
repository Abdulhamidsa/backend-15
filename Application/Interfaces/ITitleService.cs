
using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITitleService
    {
        Task<IEnumerable<TitleDto>> GetAllMoviesAsync();
        Task<IEnumerable<TitleDto>> GetAllSeriesAsync();
        Task<IEnumerable<TitleDto>> SearchTitlesAsync(long userId, string query);
        
    }
}
