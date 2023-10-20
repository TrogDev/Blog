namespace Blog.Application.Common.Commands.Categories;

using Blog.Application.Common.Abstractions;

public class CategoryGetListCommand : IPerPagePaginationCommand
{
    const int maxPageSize = 30;
    public int Page { get; set; } = 1;
    private int perPage = 10;
    public int PerPage
    {
        get { return perPage; }
        set { perPage = (value > maxPageSize) ? maxPageSize : value; }
    }
}