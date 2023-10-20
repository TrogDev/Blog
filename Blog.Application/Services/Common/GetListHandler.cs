namespace Blog.Application.Services.Common;

using Blog.Application.Common.Abstractions;

public class GetListHandler<DTO, Command>
{
    private IQuerySelector<DTO> querySelector;
    private IEnumerable<IQueryHandler<DTO, Command>> queryHandlers;

    public GetListHandler(GetListHandlerFactory<DTO, Command> factory)
    {
        this.querySelector = factory.CreateQuerySelector();
        this.queryHandlers = factory.CreateQueryHandlers();
    }

    public IQueryable<DTO> GetListQuery(Command command)
    {
        IQueryable<DTO> query = querySelector.SelectQuery();
        return handleQuery(query, command);
    }

    private IQueryable<DTO> handleQuery(IQueryable<DTO> query, Command command)
    {
        foreach (var handler in queryHandlers)
        {
            query = handler.Handle(query, command);
        }

        return query;
    }
}
