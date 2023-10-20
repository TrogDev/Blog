namespace Blog.Application.Common.Commands.Tags;

public class TagUpdateCommand
{
    public required long Id { get; set; }
    public required string Title { get; set; }
}