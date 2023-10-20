namespace Blog.Infrastructure.Auth.Common.Abstractions;

using Blog.Domain.Entities;
using Blog.Infrastructure.Auth.Common.Commands;

public interface IUserManager
{
    public Task<User> FindUser(LogInCommand command);
    public Task<User> RegisterUser(RegisterCommand command);
}
