namespace Blog.Application.Services.Posts;

using FluentValidation;

using Blog.Domain.Entities;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Posts;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Extensions;
using Blog.Application.Common.Exceptions;
using Blog.Application.Services.Common;
using Blog.Application.Services.Posts.Factories;
using Microsoft.EntityFrameworkCore;

public class PostService : IPostService
{
    private readonly IApplicationDbContext context;
    private readonly IValidator<PostCreateCommand> createCommandValidator;
    private readonly IValidator<PostUpdateCommand> updateCommandValidator;
    private readonly IValidator<PostSetTagsCommand> setTagsCommandValidator;

    public PostService(
        IApplicationDbContext context,
        IValidator<PostCreateCommand> createCommandValidator,
        IValidator<PostUpdateCommand> updateCommandValidator,
        IValidator<PostSetTagsCommand> setTagsCommandValidator
    )
    {
        this.context = context;
        this.createCommandValidator = createCommandValidator;
        this.updateCommandValidator = updateCommandValidator;
        this.setTagsCommandValidator = setTagsCommandValidator;
    }

    public async Task<PaginatedList<PostDTO>> GetList(PostGetListCommand command)
    {
        var handler = new GetListHandler<PostDTO, PostGetListCommand>(
            new PostGetListHandlerFactory(context)
        );
        return await handler.GetListQuery(command).GetPaginatedListAsync(command);
    }

    public async Task<PostDTO> GetDetail(PostGetDetailCommand command)
    {
        var handler = new GetDetailHandler<PostDTO, PostGetDetailCommand>(
            new PostGetDetailHandlerFactory(context)
        );
        return await handler.GetDetail(command);
    }

    public async Task<long> Create(PostCreateCommand command)
    {
        await createCommandValidator.ValidateAndThrowAsync(command);

        var post = new Post()
        {
            Title = command.Title,
            Content = command.Content,
            PreviewImage = command.PreviewImage,
            CategoryId = command.CategoryId
        };

        await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();

        return post.Id;
    }

    public async Task Delete(PostDeleteCommand command)
    {
        Post post = await getPost(command.Id);
        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }

    public async Task Update(PostUpdateCommand command)
    {
        await updateCommandValidator.ValidateAndThrowAsync(command);

        Post post = await getPost(command.Id);

        post.Title = command.Title;
        post.Content = command.Content;
        post.PreviewImage = command.PreviewImage;
        post.CategoryId = command.CategoryId;

        await context.SaveChangesAsync();
    }

    private async Task<Post> getPost(long postId)
    {
        Post? post = await context.Posts.FindAsync(postId);

        if (post is null)
        {
            throw new EntityNotFoundException();
        }

        return post;
    }

    public async Task SetTags(PostSetTagsCommand command)
    {
        await setTagsCommandValidator.ValidateAndThrowAsync(command);

        Post post = await getPostWithTags(command.Id);

        await addNewTags(post, command.TagIds);
        await removeOldTags(post, command.TagIds);

        await context.SaveChangesAsync();
    }

    private async Task<Post> getPostWithTags(long postId)
    {
        Post? post = await context.Posts
            .Include(e => e.Tags)
            .FirstOrDefaultAsync(e => e.Id == postId);

        if (post is null)
        {
            throw new EntityNotFoundException();
        }

        return post;
    }

    private async Task addNewTags(Post post, IEnumerable<long> tagIds)
    {
        IEnumerable<long> addedTagIds = tagIds.Except(post.Tags.Select(e => e.Id)).ToList();
        foreach (long id in addedTagIds)
        {
            Tag tag = await context.Tags.FirstAsync(e => e.Id == id);
            post.Tags.Add(tag);
        }
    }

    private async Task removeOldTags(Post post, IEnumerable<long> tagIds)
    {
        IEnumerable<long> removedTagIds = post.Tags.Select(e => e.Id).Except(tagIds).ToList();
        foreach (long id in removedTagIds)
        {
            Tag tag = await context.Tags.FirstAsync(e => e.Id == id);
            post.Tags.Remove(tag);
        }
    }
}
