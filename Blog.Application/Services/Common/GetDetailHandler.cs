namespace Blog.Application.Services.Common;

using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Abstractions;

public class GetDetailHandler<DTO, Command>
{
    private IQuerySelector<DTO> querySelector;
    private IEntitySearcher<DTO, Command> entitySearcher;

    public GetDetailHandler(GetDetailHandlerFactory<DTO, Command> factory)
    {
        this.querySelector = factory.CreateQuerySelector();
        this.entitySearcher = factory.CreateEntitySearcher();
    }

    public async Task<DTO> GetDetail(Command command)
    {
        IQueryable<DTO> query = querySelector.SelectQuery();
        DTO? entity = await entitySearcher.SearchEntity(query, command);
    
        if (entity is null)
        {
            throw new EntityNotFoundException();
        }

        return entity;
    }
}
