namespace Blog.Application.Common.Abstractions;

public abstract class GetListHandlerFactory<DTO, Command>
{
    public abstract IQuerySelector<DTO> CreateQuerySelector();
    public abstract IEnumerable<IQueryHandler<DTO, Command>> CreateQueryHandlers();
}
