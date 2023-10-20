namespace Blog.Application.Common.Commands.Posts;

public class PostUpdateCommand
{
    public required long Id;
    public required string Title { get; set; }
    public required string Content { get; set; }
    public string? PreviewImage { get; set; }
    public required long CategoryId { get; set; }
}