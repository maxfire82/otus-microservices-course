using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Common.Authentication.Services.Implementation;

public class IdentityService: IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long GetAccountId()
    {
        var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext?.User.Identity!;

        var value = identity?.Claims.SingleOrDefault(x => x.Type == "account_id")?.Value;

        return long.Parse(value ?? throw new InvalidOperationException());
    }
}