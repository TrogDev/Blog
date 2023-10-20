namespace Blog.Application.Common.Commands.Categories;

public class CategoryUpdateCommand
{
    public required long Id { get; set; }
    public required string Title { get; set; }
}