namespace Blog.Infrastructure.Auth.Common.Abstractions;

using Microsoft.EntityFrameworkCore;

using Blog.Domain.Entities;
using Blog.Infrastructure.Auth.Entities;

public interface IAuthDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Verification> Verifications { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}