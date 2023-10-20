namespace Blog.Domain.Entities;

using Blog.Domain.Common;

public class Like : BaseEntity<long>
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long PostId { get; set; }
    public Post Post { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}