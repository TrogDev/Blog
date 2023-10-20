namespace Blog.Application.Common.Abstractions;

public interface IQuerySelector<T>
{
    public IQueryable<T> SelectQuery();
}
