using Search.Domain.Models;

namespace Search.Domain.Services;

public interface IAnnouncementStore
{
    List<Announcement> Store { get; set; }
}