using Common.Extensions;
using Common.Interfaces;

namespace Search.Domain.Models.Response;

public class SearchResponse: IGenerateETag
{
    public List<Announcement> Data { get; set; }
    
    public string GenerateETag()
    {
        return Data.GetMd5Hash();
    }
}