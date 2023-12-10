using Common.Extensions;
using Common.Interfaces;

namespace Announcements.Domain.Models.Response;

public class AnnouncementsResponse: IGenerateETag
{
    public List<AnnouncementDTO> Data { get; set; }
    
    public string GenerateETag()
    {
        return Data.GetMd5Hash();
    }
}