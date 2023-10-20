namespace Blog.Application.Common.Abstractions;

using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Posts;

public interface IPostService
{
    public Task<PaginatedList<PostDTO>> GetList(PostGetListCommand command);
    public Task<PostDTO> GetDetail(PostGetDetailCommand command);
    public Task<long> Create(PostCreateCommand command);
    public Task Delete(PostDeleteCommand command);
    public Task Update(PostUpdateCommand command);
    public Task SetTags(PostSetTagsCommand command);
}