namespace Blog.Application.Services.Tags;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Microsoft.EntityFrameworkCore;

public class TagQuerySelector : IQuerySelector<TagDTO>
{
    private readonly IApplicationDbContext context;

    public TagQuerySelector(IApplicationDbContext context)
    {
        this.context = context;
    }

    public IQueryable<TagDTO> SelectQuery()
    {
        return context.Tags.Select(e => new TagDTO() { Id = e.Id, Title = e.Title });
    }
}
