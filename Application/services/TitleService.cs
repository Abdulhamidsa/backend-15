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

    public async Task<IEnumerable<TitleDto>> GetpopularTitlesAsync()
    {
        var titles = await _repository.GetpopularTitlesAsync();

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
            StartYear = t.StartYear,
            TitleType = t.TitleType,
            Poster = t.Poster

        });
    }

    public Task<IEnumerable<TitleCatalogDto>> GetTitlesAsync(string? titleType, string? genre)
    {
        throw new NotImplementedException();
    }
}
