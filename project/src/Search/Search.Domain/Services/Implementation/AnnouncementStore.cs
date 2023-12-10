using Search.Domain.Models;

namespace Search.Domain.Services.Implementation;

public class AnnouncementStore : IAnnouncementStore
{
    public List<Announcement> Store { get; set; } = new();
}