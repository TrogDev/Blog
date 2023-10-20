namespace Blog.Infrastructure.Auth.Services;

using Blog.Domain.Entities;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Data;
using Blog.Infrastructure.Auth.Common.Commands;
using Blog.Infrastructure.Auth.Common.Abstractions;
using Blog.Infrastructure.Auth.Entities;

public class AuthService : IAuthService
{
    private readonly IUserManager userManager;
    private readonly IAuthResponseProvider responseProvider;
    private readonly IUserVerificator userVerificator;
    private readonly IEmailNotifier emailNotifier;

    public AuthService(
        IUserManager userManager,
        IAuthResponseProvider responseProvider,
        IUserVerificator userVerificator,
        IEmailNotifier emailNotifier
    )
    {
        this.userManager = userManager;
        this.responseProvider = responseProvider;
        this.userVerificator = userVerificator;
        this.emailNotifier = emailNotifier;
    }

    public async Task<IAuthResponse> LogIn(LogInCommand command)
    {
        User user = await userManager.FindUser(command);
        return responseProvider.GetAuthResponse(user.Id, user.Role);
    }

    public async Task<IAuthResponse> Register(RegisterCommand command)
    {
        User user = await userManager.RegisterUser(command);
        _ = createVerification(user);
        return responseProvider.GetAuthResponse(user.Id, user.Role);
    }

    private async Task createVerification(User user)
    {
        Verification verification = await userVerificator.CreateVerification(user);
        EmailMessage message = createVerificationEmailMessage(verification);
        await emailNotifier.SendMessage(message);
    }

    private EmailMessage createVerificationEmailMessage(Verification verification)
    {
        return new EmailMessage()
        {
            To = verification.User.Email,
            Title = "Подтвердите свой аккаунт",
            Content = $"Для подтверждения перейдите по ссылке {verification.Code}"
        };
    }

    public async Task Verify(VerifyCommand command)
    {
        await userVerificator.Verify(command);
    }
}
