namespace Blog.Infrastructure.Auth.Entities;

using Blog.Domain.Common;
using Blog.Domain.Entities;

public class Verification : BaseEntity<long>
{
    public Guid Code { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long UserId { get; set; }
    public User User { get; set; }
}