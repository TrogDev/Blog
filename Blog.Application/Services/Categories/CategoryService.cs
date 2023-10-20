namespace Blog.Application.Services.Categories;

using FluentValidation;

using Blog.Domain.Entities;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Categories;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Extensions;
using Blog.Application.Common.Exceptions;
using Blog.Application.Services.Common;
using Blog.Application.Services.Categories.Factories;

public class CategoryService : ICategoryService
{
    private readonly IApplicationDbContext context;
    private readonly IValidator<CategoryCreateCommand> createCommandValidator;
    private readonly IValidator<CategoryUpdateCommand> updateCommandValidator;

    public CategoryService(
        IApplicationDbContext context,
        IValidator<CategoryCreateCommand> createCommandValidator,
        IValidator<CategoryUpdateCommand> updateCommandValidator
    )
    {
        this.context = context;
        this.createCommandValidator = createCommandValidator;
        this.updateCommandValidator = updateCommandValidator;
    }

    public async Task<PaginatedList<CategoryDTO>> GetList(CategoryGetListCommand command)
    {
        var handler = new GetListHandler<CategoryDTO, CategoryGetListCommand>(
            new CategoryGetListHandlerFactory(context)
        );
        return await handler.GetListQuery(command).GetPaginatedListAsync(command);
    }

    public async Task<CategoryDTO> GetDetail(CategoryGetDetailCommand command)
    {
        var handler = new GetDetailHandler<CategoryDTO, CategoryGetDetailCommand>(
            new CategoryGetDetailHandlerFactory(context)
        );
        return await handler.GetDetail(command);
    }

    public async Task<long> Create(CategoryCreateCommand command)
    {
        await createCommandValidator.ValidateAndThrowAsync(command);

        var category = new Category() { Title = command.Title, };

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return category.Id;
    }

    public async Task Delete(CategoryDeleteCommand command)
    {
        Category category = await getCategory(command.Id);
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task Update(CategoryUpdateCommand command)
    {
        await updateCommandValidator.ValidateAndThrowAsync(command);

        Category Category = await getCategory(command.Id);

        Category.Title = command.Title;

        await context.SaveChangesAsync();
    }

    private async Task<Category> getCategory(long CategoryId)
    {
        Category? category = await context.Categories.FindAsync(CategoryId);

        if (category is null)
        {
            throw new EntityNotFoundException();
        }

        return category;
    }
}
