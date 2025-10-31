using Application.DTOs;

namespace Application.Interfaces
{
    public interface IRatingService
    {
        Task<RateResponseDto> RateAsync(long userId, string tconst, int rating);
    }
}
