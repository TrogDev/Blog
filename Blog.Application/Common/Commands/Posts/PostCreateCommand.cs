namespace Blog.Application.Common.Commands.Posts;

public class PostCreateCommand
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? PreviewImage { get; set; }
    public required long CategoryId { get; set; }
}