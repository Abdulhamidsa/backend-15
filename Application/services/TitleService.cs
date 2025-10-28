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


}
