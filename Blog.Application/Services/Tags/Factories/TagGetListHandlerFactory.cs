namespace Blog.Application.Services.Tags.Factories;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Tags;
using Blog.Application.Services.Tags;

public class TagGetListHandlerFactory : GetListHandlerFactory<TagDTO, TagGetListCommand>
{
    private IApplicationDbContext context;

    public TagGetListHandlerFactory(IApplicationDbContext context)
    {
        this.context = context;
    }

    public override IQuerySelector<TagDTO> CreateQuerySelector()
    {
        return new TagQuerySelector(context);
    }

    public override IEnumerable<IQueryHandler<TagDTO, TagGetListCommand>> CreateQueryHandlers()
    {
        return new List<IQueryHandler<TagDTO, TagGetListCommand>>();
    }
}
