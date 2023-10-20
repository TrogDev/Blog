namespace Blog.Infrastructure.Database;

using System.Reflection;
using Microsoft.EntityFrameworkCore;

using Blog.Domain.Entities;
using Blog.Application.Common.Abstractions;
using Blog.Infrastructure.Auth.Entities;
using Blog.Infrastructure.Auth.Common.Abstractions;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IAuthDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Verification> Verifications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}