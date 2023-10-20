namespace Blog.Application.Services.Posts;

using Blog.Application.Common.Abstractions;
using Blog.Application.Common.DTO;
using Blog.Application.Common.Commands.Posts;

public class PostFilter : IQueryHandler<PostDTO, PostGetListCommand>
{
    public IQueryable<PostDTO> Handle(IQueryable<PostDTO> query, PostGetListCommand command)
    {
        if (!string.IsNullOrEmpty(command.SearchQuery))
        {
            query = filterBySearchQuery(query, command.SearchQuery);
        }
        else if (command.CategoryId is not null)
        {
            query = filterByCategoryId(query, (long)command.CategoryId);
        }
        else if (command.TagIds.Any())
        {
            query = filterByTagIds(query, command.TagIds);
        }

        return query;
    }

    private IQueryable<PostDTO> filterBySearchQuery(IQueryable<PostDTO> query, string SearchQuery)
    {
        return query.Where(e => e.Title.Contains(SearchQuery) || e.Content.Contains(SearchQuery));
    }

    private IQueryable<PostDTO> filterByCategoryId(IQueryable<PostDTO> query, long categoryId)
    {
        return query.Where(e => e.Category.Id == categoryId);
    }

    private IQueryable<PostDTO> filterByTagIds(IQueryable<PostDTO> query, IEnumerable<long> tagIds)
    {
        return query.Where(e => e.Tags.Any(tag => tagIds.Contains(tag.Id)));
    }
}
