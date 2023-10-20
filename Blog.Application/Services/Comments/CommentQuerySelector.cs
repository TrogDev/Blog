namespace Blog.Application.Services.Comments;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Microsoft.EntityFrameworkCore;

public class CommentQuerySelector : IQuerySelector<CommentDTO>
{
    private readonly IApplicationDbContext context;

    public CommentQuerySelector(IApplicationDbContext context)
    {
        this.context = context;
    }

    public IQueryable<CommentDTO> SelectQuery()
    {
        return context.Comments
            .Include(e => e.Author)
            .Select(
                e =>
                    new CommentDTO()
                    {
                        Id = e.Id,
                        Content = e.Content,
                        PostId = e.PostId,
                        Author = new UserDTO()
                        {
                            Id = e.Author.Id,
                            UserName = e.Author.UserName
                        }
                    }
            );
    }
}
