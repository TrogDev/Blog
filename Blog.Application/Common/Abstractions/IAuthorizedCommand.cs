namespace Blog.Application.Common.Abstractions;

using Blog.Domain.Enums;

public interface IAuthorizedCommand
{
    public long SenderId { get; set; }
    public Role SenderRole { get; set; }
}