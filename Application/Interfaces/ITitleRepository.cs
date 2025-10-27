
using Application.DTOs;

namespace Application.Interfaces;

public interface ITitleRepository
{
    Task<IEnumerable<TitleDto>> GetAllAsync();
}