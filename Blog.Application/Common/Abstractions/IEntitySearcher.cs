namespace Blog.Application.Common.Abstractions;

public interface IEntitySearcher<DTO, Command>
{
    public Task<DTO?> SearchEntity(IQueryable<DTO> query, Command command);
}
