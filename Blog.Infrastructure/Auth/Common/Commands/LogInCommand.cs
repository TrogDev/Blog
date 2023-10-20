namespace Blog.Infrastructure.Auth.Common.Commands;

public class LogInCommand
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}
