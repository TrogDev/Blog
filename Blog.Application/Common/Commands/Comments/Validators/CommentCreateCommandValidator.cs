namespace Blog.Application.Common.Commands.Comments.Validators;

using FluentValidation;

using Blog.Application.Common.Abstractions;

public class CommentCreateCommandValidator : AbstractValidator<CommentCreateCommand>
{
    private readonly IApplicationDbContext context;

    public CommentCreateCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Content).MaximumLength(250).NotEmpty();
        RuleFor(e => e.PostId)
            .MustAsync(isPostExists)
            .WithMessage("Пост с таким идентификатором не найден");
    }

    private async Task<bool> isPostExists(long postId, CancellationToken cancellationToken)
    {
        return await context.Posts.FindAsync(postId) is not null;
    }
}
