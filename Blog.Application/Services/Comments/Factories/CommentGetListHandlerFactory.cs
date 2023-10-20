namespace Blog.Application.Services.Comments.Factories;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Comments;
using Blog.Application.Services.Comments;

public class CommentGetListHandlerFactory : GetListHandlerFactory<CommentDTO, CommentGetListCommand>
{
    private IApplicationDbContext context;

    public CommentGetListHandlerFactory(IApplicationDbContext context)
    {
        this.context = context;
    }

    public override IQuerySelector<CommentDTO> CreateQuerySelector()
    {
        return new CommentQuerySelector(context);
    }

    public override IEnumerable<IQueryHandler<CommentDTO, CommentGetListCommand>> CreateQueryHandlers()
    {
        return new List<IQueryHandler<CommentDTO, CommentGetListCommand>>() { new CommentFilter() };
    }
}
