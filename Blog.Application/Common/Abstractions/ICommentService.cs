namespace Blog.Application.Common.Abstractions;

using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Comments;

public interface ICommentService
{
    public Task<PaginatedList<CommentDTO>> GetList(CommentGetListCommand command);
    public Task<long> Create(CommentCreateCommand command);
    public Task Delete(CommentDeleteCommand command);
}
