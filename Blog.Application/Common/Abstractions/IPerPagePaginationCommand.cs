namespace Blog.Application.Common.Abstractions;

public interface IPerPagePaginationCommand
{
    public int Page { get; set; }
    public int PerPage { get; set; }
}
