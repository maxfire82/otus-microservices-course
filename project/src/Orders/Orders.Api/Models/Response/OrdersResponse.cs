using Common.Extensions;
using Common.Interfaces;
using Orders.Domain.Models;

namespace Orders.Api.Models.Response;

public class OrdersResponse: IGenerateETag
{
    public List<OrderDTO> Data { get; set; }
    
    public string GenerateETag()
    {
        return Data.GetMd5Hash();
    }
}