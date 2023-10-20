namespace Blog.Application.Common.DTO;

public class PaginatedList<T>
{
    public required long Count { get; init; }
    public required List<T> Items { get; init; }
}
