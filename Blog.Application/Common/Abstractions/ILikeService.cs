namespace Blog.Application.Common.Abstractions;

using Blog.Application.Common.Commands.Likes;

public interface ILikeService
{
    public Task Create(LikeCreateCommand command);
    public Task Delete(LikeDeleteCommand command);
    public Task<IEnumerable<long>> CheckLikes(LikeCheckCommand command);
}