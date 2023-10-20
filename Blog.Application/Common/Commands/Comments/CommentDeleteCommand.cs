namespace Blog.Application.Common.Commands.Comments;

using Blog.Domain.Enums;
using Blog.Application.Common.Abstractions;

public class CommentDeleteCommand : IAuthorizedCommand
{
    public required long Id { get; set; }
    public required long PostId { get; set; }
    public required long SenderId { get; set; }
    public required Role SenderRole { get; set; }
}