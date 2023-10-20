namespace Blog.Application.Services.Likes;

using FluentValidation;

using Blog.Domain.Entities;
using Blog.Application.Common.Abstractions;
using Blog.Application.Common.Commands.Likes;
using Microsoft.EntityFrameworkCore;
using Blog.Application.Common.Exceptions;
using System.Collections.Generic;

public class LikeService : ILikeService
{
    private readonly IApplicationDbContext context;
    private readonly IValidator<LikeCreateCommand> createCommandValidator;

    public LikeService(IApplicationDbContext context, IValidator<LikeCreateCommand> createCommandValidator)
    {
        this.context = context;
        this.createCommandValidator = createCommandValidator;
    }

    public async Task Create(LikeCreateCommand command)
    {
        await createCommandValidator.ValidateAndThrowAsync(command);

        var like = new Like() { PostId = command.PostId, UserId = command.SenderId };

        await context.Likes.AddAsync(like);
        await context.SaveChangesAsync();
    }

    public async Task Delete(LikeDeleteCommand command)
    {
        Like like = await getLike(command.PostId, command.SenderId);
        if (isCanEdit(like, command))
        {
            context.Likes.Remove(like);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new ForbiddenException();
        }
    }

    private async Task<Like> getLike(long postId, long userId)
    {
        Like? like = await context.Likes.FirstOrDefaultAsync(
            e => e.PostId == postId & e.UserId == userId
        );

        if (like is null)
        {
            throw new EntityNotFoundException();
        }

        return like;
    }

    private bool isCanEdit(Like like, IAuthorizedCommand command)
    {
        return like.UserId == command.SenderId;
    }

    public async Task<IEnumerable<long>> CheckLikes(LikeCheckCommand command)
    {
        return await context.Likes
            .Where(e => e.UserId == command.SenderId & command.PostIds.Contains(e.PostId))
            .Select(e => e.PostId)
            .ToListAsync();
    }
}
