namespace Blog.Application.Common.Commands.Likes.Validators;

using Microsoft.EntityFrameworkCore;

using FluentValidation;

using Blog.Application.Common.Abstractions;

public class LikeCreateCommandValidator : AbstractValidator<LikeCreateCommand>
{
    private readonly IApplicationDbContext context;

    public LikeCreateCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.PostId)
            .MustAsync(isPostExists)
            .WithMessage("Пост с таким идентификатором не найден")
            .MustAsync(isNotLiked)
            .WithMessage("Вы уже лайкали этот пост");
    }

    private async Task<bool> isPostExists(long postId, CancellationToken cancellationToken)
    {
        return await context.Posts.FindAsync(postId) is not null;
    }

    private async Task<bool> isNotLiked(
        LikeCreateCommand command,
        long postId,
        CancellationToken cancellationToken
    )
    {
        return await context.Likes.FirstOrDefaultAsync(
            e => e.PostId == command.PostId & e.UserId == command.SenderId
        )
            is null;
    }
}
