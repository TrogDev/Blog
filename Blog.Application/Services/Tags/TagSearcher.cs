namespace Blog.Application.Services.Tags;

using Microsoft.EntityFrameworkCore;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Tags;

public class TagSearher : IEntitySearcher<TagDTO, TagGetDetailCommand>
{
    public async Task<TagDTO?> SearchEntity(IQueryable<TagDTO> query, TagGetDetailCommand command)
    {
        return await query.FirstOrDefaultAsync(e => e.Id == command.Id);
    }
}
