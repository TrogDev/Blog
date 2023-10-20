namespace Blog.Infrastructure.Auth.Services;

using Microsoft.EntityFrameworkCore;

using FluentValidation;

using Blog.Domain.Entities;
using Blog.Application.Common.Abstractions;
using Blog.Infrastructure.Auth.Common.Exceptions;
using Blog.Infrastructure.Auth.Common.Commands;
using Blog.Infrastructure.Auth.Common.Abstractions;

public class UserManager : IUserManager
{
    private readonly IAuthDbContext context;
    private readonly IPasswordHasher passwordHasher;
    private readonly IValidator<RegisterCommand> registerCommandValidator;

    public UserManager(
        IAuthDbContext context,
        IPasswordHasher passwordHasher,
        IValidator<RegisterCommand> registerCommandValidator
    )
    {
        this.context = context;
        this.passwordHasher = passwordHasher;
        this.registerCommandValidator = registerCommandValidator;
    }

    public async Task<User> FindUser(LogInCommand command)
    {
        string passwordHash = passwordHasher.HashPassword(command.Password);
        User? user = await context.Users.FirstOrDefaultAsync(
            e =>
                (e.Email == command.Login | e.UserName == command.Login)
                & e.PasswordHash == passwordHash
        );

        if (user is null)
        {
            throw new InvalidLogInException();
        }

        return user;
    }

    public async Task<User> RegisterUser(RegisterCommand command)
    {
        await registerCommandValidator.ValidateAndThrowAsync(command);

        string passwordHash = passwordHasher.HashPassword(command.Password);

        var user = new User()
        {
            UserName = command.UserName,
            Email = command.Email,
            PasswordHash = passwordHash
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }
}
