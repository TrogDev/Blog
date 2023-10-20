namespace Blog.Application.Common.Commands.Tags.Validators;

using Microsoft.EntityFrameworkCore;

using FluentValidation;

using Blog.Application.Common.Abstractions;

public class TagUpdateCommandValidator : AbstractValidator<TagUpdateCommand>
{
    private readonly IApplicationDbContext context;

    public TagUpdateCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Title)
            .MaximumLength(50)
            .NotEmpty()
            .MustAsync(isTagUnique)
            .WithMessage("Такой тег уже существует");
    }

    private async Task<bool> isTagUnique(string title, CancellationToken cancellationToken)
    {
        return await context.Tags.FirstOrDefaultAsync(e => e.Title == title) is null;
    }
}
