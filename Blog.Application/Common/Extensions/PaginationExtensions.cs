namespace Blog.Application.Common.Extensions;

using Microsoft.EntityFrameworkCore;

using Blog.Application.Common.DTO;
using Blog.Application.Common.Abstractions;

public static class PaginationExtensions
{
    public static async Task<PaginatedList<T>> GetPaginatedListAsync<T>(this IQueryable<T> query, IPerPagePaginationCommand command)
    {
        return new PaginatedList<T>()
        {
            Count = await query.CountAsync(),
            Items = await query
                .Skip(command.PerPage * (command.Page - 1))
                .Take(command.PerPage)
                .ToListAsync()
        };
    }
}
