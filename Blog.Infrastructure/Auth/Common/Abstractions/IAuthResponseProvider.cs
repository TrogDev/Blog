namespace Blog.Infrastructure.Auth.Common.Abstractions;

using Blog.Domain.Enums;

public interface IAuthResponseProvider
{
    public IAuthResponse GetAuthResponse(long userId, Role role);
}
