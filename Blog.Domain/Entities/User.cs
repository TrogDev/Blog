namespace Blog.Domain.Entities;

using Blog.Domain.Common;
using Blog.Domain.Enums;

public class User : BaseEntity<long>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; } = Role.Default;

    public ICollection<Like> Likes { get; set; } = new List<Like>();
}