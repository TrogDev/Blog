namespace Blog.Application.Common.DTO;

public class CommentDTO
{
    public required long Id { get; init; }
    public required string Content { get; init; }
    public required long PostId { get; init; }
    public required UserDTO Author { get; init; }
}
