namespace Blog.Application.Services.Categories;

using Microsoft.EntityFrameworkCore;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Categories;

public class CategorySearher : IEntitySearcher<CategoryDTO, CategoryGetDetailCommand>
{
    public async Task<CategoryDTO?> SearchEntity(IQueryable<CategoryDTO> query, CategoryGetDetailCommand command)
    {
        return await query.FirstOrDefaultAsync(e => e.Id == command.Id);
    }
}
