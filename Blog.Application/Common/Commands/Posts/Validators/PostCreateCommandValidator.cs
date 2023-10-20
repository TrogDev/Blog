namespace Blog.Application.Common.Commands.Posts.Validators;

using FluentValidation;

using Blog.Application.Common.Abstractions;

public class PostCreateCommandValidator : AbstractValidator<PostCreateCommand>
{
    private readonly IApplicationDbContext context;

    public PostCreateCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Title).MaximumLength(200).NotEmpty();
        RuleFor(e => e.Content).NotEmpty();
        RuleFor(e => e.PreviewImage).MaximumLength(255);
        RuleFor(e => e.CategoryId)
            .MustAsync(isCategoryExists)
            .WithMessage("Категория с таким идентификатором не найдена");
    }

    private async Task<bool> isCategoryExists(long categoryId, CancellationToken cancellationToken)
    {
        return await context.Categories.FindAsync(categoryId) is not null;
    }
}
