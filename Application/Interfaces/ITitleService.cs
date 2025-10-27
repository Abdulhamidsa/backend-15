
using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITitleService
    {
        Task<IEnumerable<TitleDto>> GetAllTitlesAsync();
    }
}
