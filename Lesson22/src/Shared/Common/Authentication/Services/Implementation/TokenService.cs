using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Common.Authentication.Services.Implementation;

public class TokenService : ITokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(IEnumerable<Claim> claims, string audience = null)
    {
        var now = DateTime.UtcNow;
        
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: _configuration.GetValueOrThrow<string>("JWT:Issuer"),
            audience: audience ?? _configuration.GetValueOrThrow<string>("JWT:Audience"),
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromMinutes(60)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                _configuration.GetValueOrThrow<string>("JWT_SECRET_KEY"))), 
                SecurityAlgorithms.HmacSha256));
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        return jwtToken;
    }
}