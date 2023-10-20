namespace Blog.Domain.Entities;

using Blog.Domain.Common;

public class Tag : BaseEntity<long>
{
    public string Title { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}