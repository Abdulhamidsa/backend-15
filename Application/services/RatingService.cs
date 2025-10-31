using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repository;

        public RatingService(IRatingRepository repository)
        {
            _repository = repository;
        }

        public async Task<RateResponseDto> RateAsync(long userId, string tconst, int rating)
        {
            var (titleId, avg) = await _repository.RateAsync(userId, tconst, rating);
            return new RateResponseDto
            {
                Tconst = titleId,
                Avg = avg
            };
        }
    }
}
