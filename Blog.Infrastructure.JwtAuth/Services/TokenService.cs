namespace Blog.Infrastructure.JwtAuth.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using Blog.Domain.Enums;
using Blog.Infrastructure.JwtAuth.Common.Abstractions;
using Blog.Infrastructure.JwtAuth.Common.Options;

public class TokenService : ITokenService
{
    private readonly JwtAuthOptions options;

    public TokenService(JwtAuthOptions options)
    {
        this.options = options;
    }

    public string CreateAccessToken(long userId, Role role)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", userId.ToString()),
            new Claim("Role", role.ToString())
        };
        var jwt = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(options.AccessLifeTimeSeconds)),
            signingCredentials: new SigningCredentials(
                options.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            )
        );
        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
        return accessToken;
    }
}
