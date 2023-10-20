namespace Blog.Infrastructure.Auth.Common.Commands;

public class RegisterCommand
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
