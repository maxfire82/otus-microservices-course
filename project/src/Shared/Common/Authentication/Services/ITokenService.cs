using System.Security.Claims;

namespace Common.Authentication.Services;

public interface ITokenService
{
    /// <summary>
    /// Создает JWT токен
    /// </summary>
    /// <returns></returns>
    string CreateToken(IEnumerable<Claim> claims, string audience = null);
}