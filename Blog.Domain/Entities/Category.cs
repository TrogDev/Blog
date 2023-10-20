namespace Blog.Domain.Entities;

using Blog.Domain.Common;

public class Category : BaseEntity<long>
{
    public string Title { get; set; }
}