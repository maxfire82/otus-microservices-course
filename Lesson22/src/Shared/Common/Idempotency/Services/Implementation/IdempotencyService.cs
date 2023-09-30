using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Common.Idempotency.Services.Implementation;

public class IdempotencyService : IIdempotencyService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdempotencyService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool ValidateOperationId(string operationId)
    {
        if (!operationId.EndsWith("\"")) {
            operationId = "\"" + operationId + "\"";
        }
        return operationId == _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.IfMatch].First();
    }
}