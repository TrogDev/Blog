namespace Blog.Application.Services.Posts;

using Microsoft.EntityFrameworkCore;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Posts;

public class PostSearher : IEntitySearcher<PostDTO, PostGetDetailCommand>
{
    public async Task<PostDTO?> SearchEntity(IQueryable<PostDTO> query, PostGetDetailCommand command)
    {
        return await query.FirstOrDefaultAsync(e => e.Id == command.Id);
    }
}
