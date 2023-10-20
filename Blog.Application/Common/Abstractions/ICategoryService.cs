namespace Blog.Application.Common.Abstractions;

using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Categories;

public interface ICategoryService
{
    public Task<PaginatedList<CategoryDTO>> GetList(CategoryGetListCommand command);
    public Task<CategoryDTO> GetDetail(CategoryGetDetailCommand command);
    public Task<long> Create(CategoryCreateCommand command);
    public Task Delete(CategoryDeleteCommand command);
    public Task Update(CategoryUpdateCommand command);
}