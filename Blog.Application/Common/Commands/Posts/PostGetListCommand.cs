namespace Blog.Application.Common.Commands.Posts;

using Blog.Application.Common.Abstractions;

public class PostGetListCommand : IPerPagePaginationCommand
{
    public IEnumerable<long> TagIds { get; set; } = new List<long>();
    public long? CategoryId { get; set; }
    public string? SearchQuery { get; set; }

    const int maxPageSize = 30;
    public int Page { get; set; } = 1;
    private int perPage = 10;
    public int PerPage
    {
        get { return perPage; }
        set { perPage = (value > maxPageSize) ? maxPageSize : value; }
    }
}