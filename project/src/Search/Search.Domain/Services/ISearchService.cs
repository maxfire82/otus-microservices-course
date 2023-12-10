using Search.Domain.Models;

namespace Search.Domain.Services;

public interface ISearchService
{
    Task<List<Announcement>> SearchAsync();
}