namespace Blog.Application.Services.Tags.Factories;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Tags;
using Blog.Application.Services.Tags;

public class TagGetDetailHandlerFactory : GetDetailHandlerFactory<TagDTO, TagGetDetailCommand>
{
    private readonly IApplicationDbContext context;

    public TagGetDetailHandlerFactory(IApplicationDbContext context)
    {
        this.context = context;
    }

    public override IQuerySelector<TagDTO> CreateQuerySelector()
    {
        return new TagQuerySelector(context);
    }

    public override IEntitySearcher<TagDTO, TagGetDetailCommand> CreateEntitySearcher()
    {
        return new TagSearher();
    }
}
