namespace Blog.Application.Services.Categories;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Microsoft.EntityFrameworkCore;

public class CategoryQuerySelector : IQuerySelector<CategoryDTO>
{
    private readonly IApplicationDbContext context;

    public CategoryQuerySelector(IApplicationDbContext context)
    {
        this.context = context;
    }

    public IQueryable<CategoryDTO> SelectQuery()
    {
        return context.Categories.Select(e => new CategoryDTO() { Id = e.Id, Title = e.Title });
    }
}
