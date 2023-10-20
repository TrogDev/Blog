namespace Blog.Application.Services.Posts.Factories;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Posts;
using Blog.Application.Services.Posts;

public class PostGetListHandlerFactory : GetListHandlerFactory<PostDTO, PostGetListCommand>
{
    private IApplicationDbContext context;

    public PostGetListHandlerFactory(IApplicationDbContext context)
    {
        this.context = context;
    }

    public override IQuerySelector<PostDTO> CreateQuerySelector()
    {
        return new PostQuerySelector(context);
    }

    public override IEnumerable<IQueryHandler<PostDTO, PostGetListCommand>> CreateQueryHandlers()
    {
        return new List<IQueryHandler<PostDTO, PostGetListCommand>>() { new PostFilter() };
    }
}
