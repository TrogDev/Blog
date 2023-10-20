namespace Blog.Infrastructure.Auth.Common.Commands.Validators;

using Microsoft.EntityFrameworkCore;

using FluentValidation;

using Blog.Application.Common.Abstractions;
using Blog.Infrastructure.Auth.Common.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IApplicationDbContext context;

    public RegisterCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.UserName)
            .MinimumLength(3)
            .MaximumLength(20)
            .MustAsync(isUniqueUserName)
            .WithMessage("Такой 'UserName' уже существует");

        RuleFor(e => e.Email)
            .MinimumLength(3)
            .MaximumLength(255)
            .EmailAddress()
            .MustAsync(isUniqueEmail)
            .WithMessage("Такой 'Email' уже существует");

        RuleFor(e => e.Password).MinimumLength(8);
    }

    private async Task<bool> isUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(e => e.UserName == userName) is null;
    }

    private async Task<bool> isUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(e => e.Email == email) is null;
    }
}
