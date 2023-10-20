namespace Blog.Application.Services.Posts;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Microsoft.EntityFrameworkCore;

public class PostQuerySelector : IQuerySelector<PostDTO>
{
    private readonly IApplicationDbContext context;

    public PostQuerySelector(IApplicationDbContext context)
    {
        this.context = context;
    }

    public IQueryable<PostDTO> SelectQuery()
    {
        return context.Posts
            .Include(e => e.Category)
            .Include(e => e.Tags)
            .Select(
                e =>
                    new PostDTO()
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Content = e.Content,
                        PreviewImage = e.PreviewImage,
                        LikesCount = e.Likes.Count(),
                        CreatedAt = e.CreatedAt,
                        Category = new CategoryDTO()
                        {
                            Id = e.Category.Id,
                            Title = e.Category.Title
                        },
                        Tags = e.Tags.Select(tag => new TagDTO() { Id = tag.Id, Title = tag.Title })
                    }
            );
    }
}
