namespace Blog.Infrastructure.Auth.Services;

using Microsoft.EntityFrameworkCore;

using Blog.Domain.Enums;
using Blog.Infrastructure.Auth.Common.Abstractions;
using Blog.Infrastructure.Auth.Common.Commands;
using Blog.Infrastructure.Auth.Entities;
using Blog.Infrastructure.Auth.Common.Exceptions;
using Blog.Domain.Entities;

public class UserVerificator : IUserVerificator
{
    private readonly IAuthDbContext context;

    public UserVerificator(IAuthDbContext context)
    {
        this.context = context;
    }

    public async Task<Verification> CreateVerification(User user)
    {
        var verification = new Verification()
        {
            UserId = user.Id
        };

        await context.Verifications.AddAsync(verification);
        await context.SaveChangesAsync();

        return verification;
    }

    public async Task Verify(VerifyCommand command)
    {
        Verification verification = await getVerification(command.Code);

        verification.User.Role = Role.Verified;

        await context.SaveChangesAsync();
    }

    private async Task<Verification> getVerification(Guid code)
    {
        Verification? verification = await context.Verifications
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Code == code);

        if (verification is null)
        {
            throw new InvalidVerificationCode();
        }

        return verification;
    }
}
