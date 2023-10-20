namespace Blog.Application.Common.Commands.Likes;

using Blog.Application.Common.Abstractions;
using Blog.Domain.Enums;

public class LikeCheckCommand : IAuthorizedCommand
{
    private ICollection<long> postIds = new List<long>();
    public ICollection<long> PostIds
    {
        get { return postIds; }
        set { postIds = value.Take(50).ToList(); }
    }

    public required long SenderId { get; set; }
    public required Role SenderRole { get; set; }
}
