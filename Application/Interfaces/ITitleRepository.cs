
using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface ITitleRepository
{
    Task<IEnumerable<Title>> GetAllAsync();
    Task<IEnumerable<Title>> SearchTitlesAsync(long userId, string query);
}