namespace Blog.Infrastructure.Auth.Services;

using System.Security.Cryptography;
using System.Text;

using Blog.Infrastructure.Auth.Common.Abstractions;

public class Sha256PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        var builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }

        return builder.ToString();
    }
}
