using Floristo.SharedAbstractions.Paging;
using Microsoft.EntityFrameworkCore;

namespace Floristo.Shared.Paging;

public static class PagedQueryExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int page, 
        int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return new PagedResult<T>(items, totalCount, page, pageSize);
    }

    public static Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, PagedQuery pagedQuery,
        CancellationToken cancellationToken = default)
        => query.ToPagedResultAsync(pagedQuery.Page, pagedQuery.PageSize, cancellationToken);
}