namespace Blog.Application.Common.Commands.Posts;

public class PostSetTagsCommand
{
    public required long Id { get; set; }
    public IEnumerable<long> TagIds { get; set; } = new List<long>();
}