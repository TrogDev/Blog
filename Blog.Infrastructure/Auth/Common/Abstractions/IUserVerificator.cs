namespace Blog.Infrastructure.Auth.Common.Abstractions;

using Blog.Domain.Entities;
using Blog.Infrastructure.Auth.Common.Commands;
using Blog.Infrastructure.Auth.Entities;

public interface IUserVerificator
{
    public Task<Verification> CreateVerification(User user);
    public Task Verify(VerifyCommand command);
}
