namespace Blog.Application.Services.Categories.Factories;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Categories;
using Blog.Application.Services.Categories;

public class CategoryGetDetailHandlerFactory : GetDetailHandlerFactory<CategoryDTO, CategoryGetDetailCommand>
{
    private readonly IApplicationDbContext context;

    public CategoryGetDetailHandlerFactory(IApplicationDbContext context)
    {
        this.context = context;
    }

    public override IQuerySelector<CategoryDTO> CreateQuerySelector()
    {
        return new CategoryQuerySelector(context);
    }

    public override IEntitySearcher<CategoryDTO, CategoryGetDetailCommand> CreateEntitySearcher()
    {
        return new CategorySearher();
    }
}
