namespace Blog.Application.Common.Commands.Likes;

using Blog.Application.Common.Abstractions;
using Blog.Domain.Enums;

public class LikeDeleteCommand : IAuthorizedCommand
{
    public required long PostId { get; set; }
    public required long SenderId { get; set; }
    public required Role SenderRole { get; set; }
}
