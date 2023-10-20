namespace Blog.Application.Common.Abstractions;

using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Tags;

public interface ITagService
{
    public Task<PaginatedList<TagDTO>> GetList(TagGetListCommand command);
    public Task<TagDTO> GetDetail(TagGetDetailCommand command);
    public Task<long> Create(TagCreateCommand command);
    public Task Delete(TagDeleteCommand command);
    public Task Update(TagUpdateCommand command);
}