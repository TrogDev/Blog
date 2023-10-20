namespace Blog.Infrastructure.JwtAuth.Services;

using Blog.Domain.Enums;
using Blog.Infrastructure.Auth.Common.Abstractions;
using Blog.Infrastructure.JwtAuth.Common.Abstractions;
using Blog.Infrastructure.JwtAuth.Common.DTO;

public class JwtAuthResponseProvider : IAuthResponseProvider
{
    private readonly ITokenService tokenService;

    public JwtAuthResponseProvider(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    public IAuthResponse GetAuthResponse(long userId, Role role)
    {
        string accessToken = tokenService.CreateAccessToken(userId, role);
        return new JwtAuthResponse() { AccessToken = accessToken };
    }
}
