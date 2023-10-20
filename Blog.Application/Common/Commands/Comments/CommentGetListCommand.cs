namespace Blog.Application.Common.Commands.Comments;

using Blog.Application.Common.Abstractions;

public class CommentGetListCommand : IPerPagePaginationCommand
{
    public long? AuthorId { get; set; }
    public long? PostId { get; set; }

    const int maxPageSize = 30;
    public int Page { get; set; } = 1;
    private int perPage = 10;
    public int PerPage
    {
        get { return perPage; }
        set { perPage = (value > maxPageSize) ? maxPageSize : value; }
    }
}