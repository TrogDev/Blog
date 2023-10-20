namespace Blog.Application.Common.Abstractions;

public interface IQueryHandler<DTO, Command>
{
    public IQueryable<DTO> Handle(IQueryable<DTO> query, Command command);
}
