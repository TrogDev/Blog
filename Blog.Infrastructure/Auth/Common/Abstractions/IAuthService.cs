namespace Blog.Infrastructure.Auth.Common.Abstractions;

using Blog.Infrastructure.Auth.Common.Commands;

public interface IAuthService
{
    public Task<IAuthResponse> LogIn(LogInCommand command);
    public Task<IAuthResponse> Register(RegisterCommand command);
    public Task Verify(VerifyCommand command);
}
