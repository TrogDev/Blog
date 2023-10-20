namespace Blog.Domain.Entities;

using Blog.Domain.Common;

public class Post : BaseEntity<long>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string? PreviewImage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
}