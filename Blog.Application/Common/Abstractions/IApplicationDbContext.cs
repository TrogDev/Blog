namespace Blog.Application.Common.Abstractions;

using Microsoft.EntityFrameworkCore;

using Blog.Domain.Entities;

public interface IApplicationDbContext
{
    DbSet<Post> Posts { get; set; }
    DbSet<Tag> Tags { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Like> Likes { get; set; }
    DbSet<Comment> Comments { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}