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
            StartYear = t.StartYear,
            TitleType = t.TitleType ?? "unknown",

        });
    }

    public async Task<IEnumerable<TitleDto>> GetAllSeriesAsync()
    {
        var series = await _repository.GetAllSeriesAsync();

        return series.Select(s => new TitleDto
        {
            Tconst = s.Tconst,
            PrimaryTitle = s.PrimaryTitle,
            StartYear = s.StartYear,
            TitleType = s.TitleType ?? "unknown"
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
            TitleType = t.TitleType ?? "unknown",
            Poster = t.Poster

        });
    }

    public async Task<IEnumerable<TitleCatalogDto>> GetTitlesAsync(string? titleType, string? genre)
    {
        var rows = await _repository.GetTitlesAsync(titleType, genre);

        return rows.Select(r => new TitleCatalogDto
        {
            Id = r.Tconst,
            Title = r.PrimaryTitle,
            Year = r.StartYear,
            Type = r.TitleType,
            Poster = r.Poster,
            Genre = r.Genre, // will be like "Action" or "Action, Thriller"

        });
    }
    public async Task<string?> GetTitleInfoById(string tconst)
    {
        // we just forward the JSON from the DB to the controller
        return await _repository.GetTitleInfoById(tconst);
    }
}


