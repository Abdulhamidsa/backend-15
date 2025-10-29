using Application.DTOs;
using Application.Interfaces;

namespace Application.Services;

public class TitleService : ITitleService
{
    private readonly ITitleRepository _repository;

    public TitleService(ITitleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TitleDto>> GetAllTitlesAsync()
    {
        var titles = await _repository.GetAllAsync();

        return titles.Select(t => new TitleDto
        {
            Tconst = t.Tconst,
            PrimaryTitle = t.PrimaryTitle,
            StartYear = t.StartYear
        });
    }

    public async Task<IEnumerable<TitleDto>> GetAllSeriesAsync()
    {
        var series = await _repository.GetAllSeriesAsync();

        return series.Select(s => new TitleDto
        {
            Tconst = s.Tconst,
            PrimaryTitle = s.PrimaryTitle,
            StartYear = s.StartYear
        });
    }

    public async Task<IEnumerable<TitleDto>> SearchTitlesAsync(long userId, string query)
    {
        var results = await _repository.SearchTitlesAsync(userId, $"%{query}%");
        return results.Select(t => new TitleDto
        {
            Tconst = t.Tconst,
            PrimaryTitle = t.PrimaryTitle,
            StartYear = t.StartYear
        });
    }

}
