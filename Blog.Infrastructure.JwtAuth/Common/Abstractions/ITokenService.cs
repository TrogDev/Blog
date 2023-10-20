namespace Blog.Infrastructure.JwtAuth.Common.Abstractions;

using Blog.Domain.Enums;

public interface ITokenService
{
    public string CreateAccessToken(long userId, Role role);
}
