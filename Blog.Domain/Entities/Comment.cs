namespace Blog.Domain.Entities;

using Blog.Domain.Common;

public class Comment : BaseEntity<long>
{
    public string Content { get; set; }

    public long AuthorId { get; set; }
    public User Author { get; set; }

    public long PostId { get; set; }
    public Post Post { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}