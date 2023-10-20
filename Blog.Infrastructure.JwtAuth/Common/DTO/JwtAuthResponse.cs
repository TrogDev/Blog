namespace Blog.Infrastructure.JwtAuth.Common.DTO;

using Blog.Infrastructure.Auth.Common.Abstractions;

public class JwtAuthResponse : IAuthResponse
{
    public required string AccessToken { get; set; }
}
