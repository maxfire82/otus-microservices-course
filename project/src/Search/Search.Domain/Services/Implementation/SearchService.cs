using Search.Domain.Models;

namespace Search.Domain.Services.Implementation;

public class SearchService : ISearchService
{
    private IAnnouncementStore _announcementStore;

    public SearchService(IAnnouncementStore announcementStore)
    {
        _announcementStore = announcementStore;
    }

    public Task<List<Announcement>> SearchAsync()
    {
        return Task.FromResult(_announcementStore.Store);
    }
}