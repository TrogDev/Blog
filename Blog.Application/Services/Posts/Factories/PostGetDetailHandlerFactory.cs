namespace Blog.Application.Services.Posts.Factories;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Posts;
using Blog.Application.Services.Posts;

public class PostGetDetailHandlerFactory : GetDetailHandlerFactory<PostDTO, PostGetDetailCommand>
{
    private readonly IApplicationDbContext context;

    public PostGetDetailHandlerFactory(IApplicationDbContext context)
    {
        this.context = context;
    }

    public override IQuerySelector<PostDTO> CreateQuerySelector()
    {
        return new PostQuerySelector(context);
    }

    public override IEntitySearcher<PostDTO, PostGetDetailCommand> CreateEntitySearcher()
    {
        return new PostSearher();
    }
}
