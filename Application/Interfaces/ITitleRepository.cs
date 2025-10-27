using Domain.Models;

namespace Application.Interfaces;

public interface ITitleRepository
{
    Task<IEnumerable<Title>> GetAllAsync();
}