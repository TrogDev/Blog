namespace Blog.Application.Common.Commands.Categories.Validators;

using Microsoft.EntityFrameworkCore;

using FluentValidation;

using Blog.Application.Common.Abstractions;

public class CategoryUpdateCommandValidator : AbstractValidator<CategoryUpdateCommand>
{
    private readonly IApplicationDbContext context;

    public CategoryUpdateCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Title)
            .MaximumLength(50)
            .NotEmpty()
            .MustAsync(isCommandUnique)
            .WithMessage("Такая категория уже существует");
    }

    private async Task<bool> isCommandUnique(string title, CancellationToken cancellationToken)
    {
        return await context.Categories.FirstOrDefaultAsync(e => e.Title == title) is null;
    }
}
