namespace Blog.Application.Common.Abstractions;

public abstract class GetDetailHandlerFactory<DTO, Command>
{
    public abstract IQuerySelector<DTO> CreateQuerySelector();
    public abstract IEntitySearcher<DTO, Command> CreateEntitySearcher();
}
