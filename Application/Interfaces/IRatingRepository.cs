namespace Application.Interfaces
{
    public interface IRatingRepository
    {
        Task<(string Tconst, decimal Avg)> RateAsync(long userId, string tconst, int rating);
    }
}
