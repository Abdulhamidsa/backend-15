
using Application.DTOs;
using Application.RowClasses;
using Domain.Models;

namespace Application.Interfaces;

public interface ITitleRepository
{
    Task<IEnumerable<Title>> GetpopularTitlesAsync();
    Task<IEnumerable<Title>> GetAllSeriesAsync();
    Task<IEnumerable<Title>> SearchTitlesAsync(long userId, string query);
    Task<List<TitleRow>> GetTitlesAsync(string? titleType, string? genre);
    Task<string?> GetTitleInfoById(string tconst);

}