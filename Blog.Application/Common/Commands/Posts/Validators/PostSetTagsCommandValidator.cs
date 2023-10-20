namespace Blog.Application.Common.Commands.Posts.Validators;

using FluentValidation;

using Blog.Application.Common.Abstractions;
using Microsoft.EntityFrameworkCore;

public class PostSetTagsCommandValidator : AbstractValidator<PostSetTagsCommand>
{
    private readonly IApplicationDbContext context;

    public PostSetTagsCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.TagIds).MustAsync(isAllTagsExists).WithMessage("Не все теги были найдены");
    }

    private async Task<bool> isAllTagsExists(
        IEnumerable<long> tagIds,
        CancellationToken cancellationToken
    )
    {
        return await context.Categories.Where(e => tagIds.Contains(e.Id)).CountAsync()
            == tagIds.Count();
    }
}
