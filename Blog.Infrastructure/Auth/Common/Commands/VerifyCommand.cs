namespace Blog.Infrastructure.Auth.Common.Commands;

public class VerifyCommand
{
    public required Guid Code { get; set; }
}
