namespace Blog.Application.Common.Commands.Comments;

using Blog.Application.Common.Abstractions;
using Blog.Domain.Enums;

public class CommentCreateCommand : IAuthorizedCommand
{
    public required string Content { get; set; }
    public required long PostId { get; set; }
    public required long SenderId { get; set; }
    public required Role SenderRole { get; set; }
}