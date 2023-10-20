namespace Blog.Application.Services.Categories.Factories;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Categories;
using Blog.Application.Services.Categories;

public class CategoryGetListHandlerFactory : GetListHandlerFactory<CategoryDTO, CategoryGetListCommand>
{
    private IApplicationDbContext context;

    public CategoryGetListHandlerFactory(IApplicationDbContext context)
    {
        this.context = context;
    }

    public override IQuerySelector<CategoryDTO> CreateQuerySelector()
    {
        return new CategoryQuerySelector(context);
    }

    public override IEnumerable<IQueryHandler<CategoryDTO, CategoryGetListCommand>> CreateQueryHandlers()
    {
        return new List<IQueryHandler<CategoryDTO, CategoryGetListCommand>>();
    }
}
