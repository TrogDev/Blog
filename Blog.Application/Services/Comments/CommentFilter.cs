namespace Blog.Application.Services.Comments;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Comments;

public class CommentFilter : IQueryHandler<CommentDTO, CommentGetListCommand>
{
    public IQueryable<CommentDTO> Handle(IQueryable<CommentDTO> query, CommentGetListCommand command)
    {
        if (command.AuthorId is not null)
        {
            query = filterByAuthorId(query, (long)command.AuthorId);
        }
        else if (command.PostId is not null)
        {
            query = filterByPostId(query, (long)command.PostId);
        }

        return query;
    }

    private IQueryable<CommentDTO> filterByAuthorId(IQueryable<CommentDTO> query, long authorId)
    {
        return query.Where(e => e.Author.Id == authorId);
    }

    private IQueryable<CommentDTO> filterByPostId(IQueryable<CommentDTO> query, long postId)
    {
        return query.Where(e => e.PostId == postId);
    }
}
