namespace Blog.Infrastructure.Auth.Common.Abstractions;

public interface IPasswordHasher
{
    public string HashPassword(string password);
}
